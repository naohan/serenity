namespace serenity.Application.DTOs;

public class MentalWellbeingMetricDto
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public DateOnly Date { get; set; }
    public sbyte? StressLevel { get; set; }
    public sbyte? EnergyLevel { get; set; }
    public sbyte? ConcentrationLevel { get; set; }
    public sbyte? SatisfactionLevel { get; set; }
    public decimal? SleepDuration { get; set; }
    public string? SleepQuality { get; set; }
    public int? MeditationMinutes { get; set; }
    public int? MeditationSessions { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}



