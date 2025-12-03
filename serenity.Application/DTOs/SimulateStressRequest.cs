namespace serenity.Application.DTOs;

/// <summary>
/// Request DTO for simulating stress levels.
/// </summary>
public class SimulateStressRequest
{
    public int PatientId { get; set; }
    public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public int? HoursToSimulate { get; set; } // Si es null, simula 24 horas
}

