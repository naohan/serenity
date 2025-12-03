namespace serenity.Application.DTOs;

/// <summary>
/// Request DTO for generating AI insights for a patient.
/// </summary>
public class GenerateInsightsRequest
{
    public int PatientId { get; set; }
    public bool IncludeHistoricalData { get; set; } = true;
    public int? DaysOfHistory { get; set; } // If null, includes all history
}

