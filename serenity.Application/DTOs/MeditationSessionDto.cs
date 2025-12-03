namespace serenity.Application.DTOs;

public class MeditationSessionDto
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public DateOnly SessionDate { get; set; }
    public int DurationMinutes { get; set; }
    public string? Type { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; }
}




