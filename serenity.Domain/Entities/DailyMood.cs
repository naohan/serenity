using System;
using System.Collections.Generic;

namespace serenity.Infrastructure;

public partial class DailyMood
{
    public int Id { get; set; }

    public int PatientId { get; set; }

    public DateOnly Date { get; set; }

    public sbyte Mood { get; set; }

    public string? Note { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Patient Patient { get; set; } = null!;
}
