namespace serenity.Application.DTOs;

public class PatientNoteDto
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public int PsychologistId { get; set; }
    public DateOnly Date { get; set; }
    public string Content { get; set; } = string.Empty;
    public sbyte Mood { get; set; }
    public string? AiDiagnosis { get; set; }
    public bool? NeedsFollowUp { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}




