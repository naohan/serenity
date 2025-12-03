namespace serenity.Application.DTOs;

public class CreateChatMessageRequest
{
    public int PatientId { get; set; }
    public int PsychologistId { get; set; }
    public string MessageText { get; set; } = string.Empty;
    public bool IsFromPsychologist { get; set; }
}



