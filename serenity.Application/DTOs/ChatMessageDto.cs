namespace serenity.Application.DTOs;

public class ChatMessageDto
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public int PsychologistId { get; set; }
    public string MessageText { get; set; } = string.Empty;
    public bool IsFromPsychologist { get; set; }
    public DateTime SentAt { get; set; }
    public DateTime? ReadAt { get; set; }
}



