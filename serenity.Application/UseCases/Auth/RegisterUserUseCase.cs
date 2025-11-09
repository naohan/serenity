using serenity.Application.DTOs;
using serenity.Application.Interfaces;
using serenity.Application.UseCases.Users;
using serenity.Infrastructure;

namespace serenity.Application.UseCases.Auth;

public class RegisterUserUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;

    public RegisterUserUseCase(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
    }

    public async Task<UserDto> ExecuteAsync(RegisterRequest request, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(request.Email))
        {
            throw new ArgumentException("El email es obligatorio", nameof(request.Email));
        }

        if (string.IsNullOrWhiteSpace(request.Password))
        {
            throw new ArgumentException("La contraseña es obligatoria", nameof(request.Password));
        }

        var existingByEmail = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
        if (existingByEmail is not null)
        {
            throw new InvalidOperationException($"El email '{request.Email}' ya está registrado.");
        }

        var user = new User
        {
            Name = request.Name,
            Email = request.Email,
            PasswordHash = _passwordHasher.HashPassword(request.Password),
            Role = request.Role,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsActive = true
        };

        await _userRepository.AddAsync(user, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return user.ToDto();
    }
}

