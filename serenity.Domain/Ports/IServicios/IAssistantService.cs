namespace serenity.Domain.Ports.IServicios;

/// <summary>
/// AI Assistant service port for generating insights, recommendations, and analysis.
/// </summary>
public interface IAssistantService
{
    /// <summary>
    /// Generates AI-powered insights and recommendations for a patient based on their history.
    /// </summary>
    Task<AIInsightResult> GenerateInsightsForPatientAsync(int patientId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Analyzes patient notes and generates diagnostic insights.
    /// </summary>
    Task<AIDiagnosisResult> AnalyzeNotesAsync(int noteId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Analyzes wellbeing trends for a patient over a date range.
    /// </summary>
    Task<AITrendAnalysisResult> AnalyzeWellbeingTrendsAsync(int patientId, DateOnly startDate, DateOnly endDate, CancellationToken cancellationToken = default);
}

/// <summary>
/// Result of AI insight generation for a patient.
/// </summary>
public class AIInsightResult
{
    public List<string> Insights { get; set; } = new();
    public List<string> Recommendations { get; set; } = new();
    public string? Summary { get; set; }
}

/// <summary>
/// Result of AI diagnosis from notes.
/// </summary>
public class AIDiagnosisResult
{
    public string? Diagnosis { get; set; }
    public List<string> Suggestions { get; set; } = new();
    public string? Analysis { get; set; }
}

/// <summary>
/// Result of wellbeing trends analysis.
/// </summary>
public class AITrendAnalysisResult
{
    public string? TrendAnalysis { get; set; }
    public List<string> KeyFindings { get; set; } = new();
    public List<string> Recommendations { get; set; } = new();
}

