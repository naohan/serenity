namespace serenity.Application.DTOs;

/// <summary>
/// DTO para la creación de usuarios por parte de un administrador.
/// </summary>
public class CreateUserRequest
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public sbyte Role { get; set; }
    public bool IsActive { get; set; } = true;
}

