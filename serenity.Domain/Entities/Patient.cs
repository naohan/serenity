using System;
using System.Collections.Generic;

namespace serenity.Infrastructure;

public partial class Patient
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? PsychologistId { get; set; }

    public string Name { get; set; } = null!;

    public string? AvatarUrl { get; set; }

    public int? Age { get; set; }

    public string? Status { get; set; }

    public string? Diagnosis { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual ICollection<ChatMessage> ChatMessages { get; set; } = new List<ChatMessage>();

    public virtual ICollection<DailyMood> DailyMoods { get; set; } = new List<DailyMood>();

    public virtual ICollection<EmotionalState> EmotionalStates { get; set; } = new List<EmotionalState>();

    public virtual ICollection<InsightsAndRecommendation> InsightsAndRecommendations { get; set; } = new List<InsightsAndRecommendation>();

    public virtual ICollection<MeditationSession> MeditationSessions { get; set; } = new List<MeditationSession>();

    public virtual ICollection<MentalWellbeingMetric> MentalWellbeingMetrics { get; set; } = new List<MentalWellbeingMetric>();

    public virtual ICollection<MoodMetric> MoodMetrics { get; set; } = new List<MoodMetric>();

    public virtual ICollection<PatientNote> PatientNotes { get; set; } = new List<PatientNote>();

    public virtual ICollection<PatientReport> PatientReports { get; set; } = new List<PatientReport>();

    public virtual ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();

    public virtual Psychologist? Psychologist { get; set; }

    public virtual ICollection<StressLevelsByTime> StressLevelsByTimes { get; set; } = new List<StressLevelsByTime>();

    public virtual User? User { get; set; }
}
