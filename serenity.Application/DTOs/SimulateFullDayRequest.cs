namespace serenity.Application.DTOs;

/// <summary>
/// Request DTO for simulating a full day of metrics.
/// </summary>
public class SimulateFullDayRequest
{
    public int PatientId { get; set; }
    public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Now);
}

