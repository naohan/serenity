using System;
using System.Collections.Generic;

namespace serenity.Infrastructure;

public partial class EmotionalState
{
    public int Id { get; set; }

    public int PatientId { get; set; }

    public DateOnly Date { get; set; }

    public string? EmotionalState1 { get; set; }

    public decimal? Value { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Patient Patient { get; set; } = null!;
}
