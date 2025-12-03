namespace serenity.Application.DTOs;

/// <summary>
/// Response DTO for AI-generated trend analysis.
/// </summary>
public class TrendAnalysisResponseDto
{
    public int PatientId { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public string? TrendAnalysis { get; set; }
    public List<string> KeyFindings { get; set; } = new();
    public List<string> Recommendations { get; set; } = new();
    public DateTime GeneratedAt { get; set; }
}


