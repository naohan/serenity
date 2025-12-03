using Google.Apis.Auth;
using Microsoft.Extensions.Configuration;
using serenity.Application.DTOs;
using serenity.Application.DTOs.Auth;
using serenity.Application.Interfaces;
using serenity.Application.UseCases.Users;
using serenity.Infrastructure;

namespace serenity.Application.UseCases.Auth;

/// <summary>
/// Use case for handling Google authentication login.
/// </summary>
public class GoogleLoginUseCase
{
    private readonly IGoogleAuthService _googleAuthService;
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _configuration;

    public GoogleLoginUseCase(
        IGoogleAuthService googleAuthService,
        IUserRepository userRepository,
        IJwtService jwtService,
        IUnitOfWork unitOfWork,
        IConfiguration configuration)
    {
        _googleAuthService = googleAuthService;
        _userRepository = userRepository;
        _jwtService = jwtService;
        _unitOfWork = unitOfWork;
        _configuration = configuration;
    }

    public async Task<AuthResponse> ExecuteAsync(GoogleLoginDto request, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(request.IdToken))
        {
            throw new ArgumentException("ID token is required.", nameof(request.IdToken));
        }

        // Validar el token de Google y obtener el payload
        var payload = await _googleAuthService.ValidateGoogleTokenAsync(request.IdToken, cancellationToken);

        // Extraer información del payload
        var email = payload.Email ?? throw new InvalidOperationException("Email not found in Google token.");
        var name = payload.Name ?? email.Split('@')[0];
        var googleId = payload.Subject ?? throw new InvalidOperationException("Google ID (sub) not found in token.");
        var avatar = payload.Picture;

        User? user = null;

        // 1. Buscar primero por google_id
        if (!string.IsNullOrWhiteSpace(googleId))
        {
            user = await _userRepository.GetByGoogleIdAsync(googleId, cancellationToken);
        }

        // 2. Si no existe por google_id, buscar por email
        if (user == null)
        {
            user = await _userRepository.GetByEmailAsync(email, cancellationToken);
        }

        if (user != null)
        {
            // Usuario ya existe
            if (user.IsActive == false)
            {
                throw new InvalidOperationException("Tu cuenta está desactivada. Contacta al administrador.");
            }

            // Actualizar información de Google si no estaba vinculada
            if (string.IsNullOrWhiteSpace(user.GoogleId) && !string.IsNullOrWhiteSpace(googleId))
            {
                user.GoogleId = googleId;
            }

            // Actualizar avatar si está disponible
            if (!string.IsNullOrWhiteSpace(avatar))
            {
                user.AvatarUrl = avatar;
            }

            user.UpdatedAt = DateTime.Now;
            await _userRepository.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
        else
        {
            // Crear nuevo usuario
            var now = DateTime.Now;
            user = new User
            {
                Name = name,
                Email = email,
                GoogleId = googleId,
                AvatarUrl = avatar,
                Role = 0, // Paciente por defecto
                PasswordHash = string.Empty, // Sin contraseña para usuarios de Google
                IsActive = true,
                CreatedAt = now,
                UpdatedAt = now
            };

            await _userRepository.AddAsync(user, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        // Generar JWT usando el servicio existente
        var token = _jwtService.GenerateToken(user);
        var expirationMinutes = int.TryParse(_configuration["Jwt:ExpirationMinutes"], out var minutes) ? minutes : 60;

        return new AuthResponse
        {
            Token = token,
            ExpiresAtUtc = DateTime.UtcNow.AddMinutes(expirationMinutes),
            User = user.ToDto()
        };
    }
}
