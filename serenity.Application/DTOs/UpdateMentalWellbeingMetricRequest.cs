namespace serenity.Application.DTOs;

public class UpdateMentalWellbeingMetricRequest
{
    public DateOnly? Date { get; set; }
    public sbyte? StressLevel { get; set; }
    public sbyte? EnergyLevel { get; set; }
    public sbyte? ConcentrationLevel { get; set; }
    public sbyte? SatisfactionLevel { get; set; }
    public decimal? SleepDuration { get; set; }
    public string? SleepQuality { get; set; }
    public int? MeditationMinutes { get; set; }
    public int? MeditationSessions { get; set; }
}




