namespace serenity.Domain.Ports.IServicios;

/// <summary>
/// JWT token service port for authentication.
/// </summary>
public interface IJwtService
{
    string GenerateToken(object user);
    string? ValidateToken(string token);
}

