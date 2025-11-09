using Microsoft.Extensions.Configuration;
using serenity.Application.DTOs;
using serenity.Application.Interfaces;
using serenity.Application.UseCases.Users;

namespace serenity.Application.UseCases.Auth;

public class LoginUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtService _jwtService;
    private readonly IConfiguration _configuration;

    public LoginUseCase(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IJwtService jwtService,
        IConfiguration configuration)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtService = jwtService;
        _configuration = configuration;
    }

    public async Task<AuthResponse> ExecuteAsync(LoginRequest request, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
        {
            throw new ArgumentException("Correo y contraseña son obligatorios.");
        }

        var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
        if (user is null || user.IsActive == false)
        {
            throw new InvalidOperationException("Credenciales inválidas.");
        }

        var passwordValid = _passwordHasher.VerifyPassword(request.Password, user.PasswordHash);
        if (!passwordValid)
        {
            throw new InvalidOperationException("Credenciales inválidas.");
        }

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

