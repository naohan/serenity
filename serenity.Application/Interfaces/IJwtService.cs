using serenity.Infrastructure;

namespace serenity.Application.Interfaces;

/// <summary>
/// Token service abstraction for issuing and validating JWT tokens.
/// </summary>
public interface IJwtService
{
    string GenerateToken(User user);
    string? ValidateToken(string token);
}

