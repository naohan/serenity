namespace serenity.Application.DTOs;

public class UpdateMeditationSessionRequest
{
    public DateOnly? SessionDate { get; set; }
    public int? DurationMinutes { get; set; }
    public string? Type { get; set; }
    public string? Notes { get; set; }
}




