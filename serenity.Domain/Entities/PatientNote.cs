using System;
using System.Collections.Generic;

namespace serenity.Infrastructure;

public partial class PatientNote
{
    public int Id { get; set; }

    public int PatientId { get; set; }

    public int PsychologistId { get; set; }

    public DateOnly Date { get; set; }

    public string Content { get; set; } = null!;

    public sbyte Mood { get; set; }

    public string? AiDiagnosis { get; set; }

    public bool? NeedsFollowUp { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<NoteSuggestion> NoteSuggestions { get; set; } = new List<NoteSuggestion>();

    public virtual Patient Patient { get; set; } = null!;

    public virtual Psychologist Psychologist { get; set; } = null!;
}
