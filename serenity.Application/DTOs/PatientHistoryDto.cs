namespace serenity.Application.DTOs;

/// <summary>
/// DTO representing a patient's complete history for AI analysis.
/// </summary>
public class PatientHistoryDto
{
    public int PatientId { get; set; }
    public string? PatientName { get; set; }
    public int? Age { get; set; }
    public string? Gender { get; set; }
    public string? Diseases { get; set; }
    public string? MedicalAdvice { get; set; }
    public string? InitialObjective { get; set; }
    public List<DailyMoodHistoryDto> DailyMoods { get; set; } = new();
    public List<EmotionalStateHistoryDto> EmotionalStates { get; set; } = new();
    public List<MoodMetricHistoryDto> MoodMetrics { get; set; } = new();
    public List<MentalWellbeingMetricHistoryDto> MentalWellbeingMetrics { get; set; } = new();
    public List<MeditationSessionHistoryDto> MeditationSessions { get; set; } = new();
    public List<StressLevelHistoryDto> StressLevels { get; set; } = new();
    public List<PatientNoteHistoryDto> Notes { get; set; } = new();
    public List<AppointmentHistoryDto> Appointments { get; set; } = new();
}

public class DailyMoodHistoryDto
{
    public DateOnly Date { get; set; }
    public string? Mood { get; set; }
    public string? Notes { get; set; }
}

public class EmotionalStateHistoryDto
{
    public DateOnly Date { get; set; }
    public string? State { get; set; }
    public string? Description { get; set; }
}

public class MoodMetricHistoryDto
{
    public DateOnly Date { get; set; }
    public decimal? Score { get; set; }
    public string? Notes { get; set; }
}

public class MentalWellbeingMetricHistoryDto
{
    public DateOnly Date { get; set; }
    public decimal? Score { get; set; }
    public string? Notes { get; set; }
}

public class MeditationSessionHistoryDto
{
    public DateOnly Date { get; set; }
    public int? DurationMinutes { get; set; }
    public string? Type { get; set; }
}

public class StressLevelHistoryDto
{
    public DateOnly Date { get; set; }
    public TimeOnly TimeOfDay { get; set; }
    public sbyte StressLevel { get; set; }
}

public class PatientNoteHistoryDto
{
    public int Id { get; set; }
    public DateOnly Date { get; set; }
    public string? Content { get; set; }
    public string? Type { get; set; }
}

public class AppointmentHistoryDto
{
    public DateOnly Date { get; set; }
    public TimeOnly? Time { get; set; }
    public string? Status { get; set; }
    public string? Notes { get; set; }
}

