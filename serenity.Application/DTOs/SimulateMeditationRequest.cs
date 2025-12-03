namespace serenity.Application.DTOs;

/// <summary>
/// Request DTO for simulating meditation sessions.
/// </summary>
public class SimulateMeditationRequest
{
    public int PatientId { get; set; }
    public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public int? NumberOfSessions { get; set; } // Si es null, genera 1-3 sesiones aleatorias
}


