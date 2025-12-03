namespace serenity.Application.DTOs;

public class CreateInsightsAndRecommendationRequest
{
    public int PatientId { get; set; }
    public int? PsychologistId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? Type { get; set; }
    public string? Priority { get; set; }
}




