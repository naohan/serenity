using System;
using System.Collections.Generic;

namespace serenity.Infrastructure;

public partial class InsightsAndRecommendation
{
    public int Id { get; set; }

    public int PatientId { get; set; }

    public int? PsychologistId { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string? Type { get; set; }

    public string? Priority { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Patient Patient { get; set; } = null!;

    public virtual Psychologist? Psychologist { get; set; }
}
