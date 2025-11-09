namespace serenity.Application.DTOs;

/// <summary>
/// Representación simplificada del usuario para exponer vía API.
/// </summary>
public class UserDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public sbyte Role { get; set; }
    public bool? IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
}

