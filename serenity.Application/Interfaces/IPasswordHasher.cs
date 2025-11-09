namespace serenity.Application.Interfaces;

/// <summary>
/// Abstraction over password hashing utilities.
/// </summary>
public interface IPasswordHasher
{
    string HashPassword(string password);
    bool VerifyPassword(string password, string passwordHash);
}

