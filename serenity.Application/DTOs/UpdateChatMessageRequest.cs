namespace serenity.Application.DTOs;

public class UpdateChatMessageRequest
{
    public string? MessageText { get; set; }
    public bool? IsFromPsychologist { get; set; }
    public DateTime? ReadAt { get; set; }
}



