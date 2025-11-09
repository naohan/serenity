using System;
using System.Collections.Generic;

namespace serenity.Infrastructure;

public partial class PatientReport
{
    public int Id { get; set; }

    public int PatientId { get; set; }

    public int PsychologistId { get; set; }

    public string? Title { get; set; }

    public string Diagnosis { get; set; } = null!;

    public string? AnxietyLevel { get; set; }

    public string? Recommendations { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Patient Patient { get; set; } = null!;

    public virtual Psychologist Psychologist { get; set; } = null!;
}
