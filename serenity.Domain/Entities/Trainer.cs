using System;
using System.Collections.Generic;

namespace serenity.Infrastructure;

public partial class Trainer
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string? Specialization { get; set; }

    public string? Bio { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual User User { get; set; } = null!;
}
