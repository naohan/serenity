namespace serenity.Application.DTOs;

/// <summary>
/// Datos necesarios para registrar un nuevo usuario.
/// </summary>
public class RegisterRequest
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public sbyte Role { get; set; } = 0;
}

