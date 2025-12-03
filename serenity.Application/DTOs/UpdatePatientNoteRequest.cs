namespace serenity.Application.DTOs;

public class UpdatePatientNoteRequest
{
    public DateOnly? Date { get; set; }
    public string? Content { get; set; }
    public sbyte? Mood { get; set; }
    public string? AiDiagnosis { get; set; }
    public bool? NeedsFollowUp { get; set; }
}




