namespace serenity.Application.DTOs;

public class CreatePatientNoteRequest
{
    public int PatientId { get; set; }
    public int PsychologistId { get; set; }
    public DateOnly Date { get; set; }
    public string Content { get; set; } = string.Empty;
    public sbyte Mood { get; set; }
    public string? AiDiagnosis { get; set; }
    public bool? NeedsFollowUp { get; set; }
}




