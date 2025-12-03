namespace serenity.Application.DTOs;

public class UpdateInsightsAndRecommendationRequest
{
    public int? PsychologistId { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Type { get; set; }
    public string? Priority { get; set; }
}



