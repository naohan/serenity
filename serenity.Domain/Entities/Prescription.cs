using System;
using System.Collections.Generic;

namespace serenity.Infrastructure;

public partial class Prescription
{
    public int Id { get; set; }

    public int PatientId { get; set; }

    public int PsychologistId { get; set; }

    public string MedicationName { get; set; } = null!;

    public string? Dosage { get; set; }

    public string? Frequency { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public string? Instructions { get; set; }

    public string? Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Patient Patient { get; set; } = null!;

    public virtual Psychologist Psychologist { get; set; } = null!;
}
