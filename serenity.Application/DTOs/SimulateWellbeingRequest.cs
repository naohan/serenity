namespace serenity.Application.DTOs;

/// <summary>
/// Request DTO for simulating wellbeing metrics.
/// </summary>
public class SimulateWellbeingRequest
{
    public int PatientId { get; set; }
    public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Now);
}

