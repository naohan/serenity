using System;
using System.Collections.Generic;

namespace serenity.Infrastructure;

public partial class Psychologist
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string? Country { get; set; }

    public string? Location { get; set; }

    public string? CollegeNumber { get; set; }

    public string? Specialization { get; set; }

    public int? WeeklyScore { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual ICollection<ChatMessage> ChatMessages { get; set; } = new List<ChatMessage>();

    public virtual ICollection<InsightsAndRecommendation> InsightsAndRecommendations { get; set; } = new List<InsightsAndRecommendation>();

    public virtual ICollection<PatientNote> PatientNotes { get; set; } = new List<PatientNote>();

    public virtual ICollection<PatientReport> PatientReports { get; set; } = new List<PatientReport>();

    public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();

    public virtual ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();

    public virtual User User { get; set; } = null!;
}
