namespace serenity.Domain.Ports.IServicios;

/// <summary>
/// Password hashing service port.
/// </summary>
public interface IPasswordHasher
{
    string HashPassword(string password);
    bool VerifyPassword(string password, string passwordHash);
}



