namespace serenity.Application.DTOs;

/// <summary>
/// Respuesta genérica para las operaciones de autenticación.
/// </summary>
public class AuthResponse
{
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiresAtUtc { get; set; }
    public UserDto User { get; set; } = new();
}

