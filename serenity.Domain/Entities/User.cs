using System;
using System.Collections.Generic;

namespace serenity.Infrastructure;

public partial class User
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Name { get; set; } = null!;

    public sbyte Role { get; set; }

    public string? AvatarUrl { get; set; }

    public string? GoogleId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool? IsActive { get; set; }

    public virtual Patient? Patient { get; set; }

    public virtual Psychologist? Psychologist { get; set; }

    public virtual Trainer? Trainer { get; set; }
}
