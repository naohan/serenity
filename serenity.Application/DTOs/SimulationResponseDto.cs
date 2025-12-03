namespace serenity.Application.DTOs;

/// <summary>
/// Response DTO for simulation operations.
/// </summary>
public class SimulationResponseDto
{
    public int PatientId { get; set; }
    public DateOnly Date { get; set; }
    public string SimulationType { get; set; } = string.Empty;
    public int RecordsCreated { get; set; }
    public string Message { get; set; } = string.Empty;
}


