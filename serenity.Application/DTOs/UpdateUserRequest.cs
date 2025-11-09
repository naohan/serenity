namespace serenity.Application.DTOs;

/// <summary>
/// DTO para actualizar los datos de un usuario existente.
/// </summary>
public class UpdateUserRequest
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public sbyte Role { get; set; }
    public bool IsActive { get; set; } = true;
    public string? Password { get; set; }
}

