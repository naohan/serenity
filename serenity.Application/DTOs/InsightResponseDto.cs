namespace serenity.Application.DTOs;

/// <summary>
/// Response DTO for AI-generated insights.
/// </summary>
public class InsightResponseDto
{
    public int PatientId { get; set; }
    public List<string> Insights { get; set; } = new();
    public List<string> Recommendations { get; set; } = new();
    public string? Summary { get; set; }
    public DateTime GeneratedAt { get; set; }
}


