using System;
using System.Collections.Generic;

namespace serenity.Infrastructure;

public partial class MeditationSession
{
    public int Id { get; set; }

    public int PatientId { get; set; }

    public DateOnly SessionDate { get; set; }

    public int DurationMinutes { get; set; }

    public string? Type { get; set; }

    public string? Notes { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Patient Patient { get; set; } = null!;
}
