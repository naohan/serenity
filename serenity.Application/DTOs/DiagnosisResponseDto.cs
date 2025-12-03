namespace serenity.Application.DTOs;

/// <summary>
/// Response DTO for AI-generated diagnosis from notes.
/// </summary>
public class DiagnosisResponseDto
{
    public int NoteId { get; set; }
    public string? Diagnosis { get; set; }
    public List<string> Suggestions { get; set; } = new();
    public string? Analysis { get; set; }
    public DateTime GeneratedAt { get; set; }
}


