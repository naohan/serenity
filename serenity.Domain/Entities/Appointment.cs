using System;
using System.Collections.Generic;

namespace serenity.Infrastructure;

public partial class Appointment
{
    public int Id { get; set; }

    public int PatientId { get; set; }

    public int PsychologistId { get; set; }

    public DateOnly AppointmentDate { get; set; }

    public TimeOnly AppointmentTime { get; set; }

    public int Duration { get; set; }

    public string? Type { get; set; }

    public string? Status { get; set; }

    public string? Notes { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Patient Patient { get; set; } = null!;

    public virtual Psychologist Psychologist { get; set; } = null!;
}
