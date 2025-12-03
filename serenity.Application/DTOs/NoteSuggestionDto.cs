namespace serenity.Application.DTOs;

public class NoteSuggestionDto
{
    public int Id { get; set; }
    public int NoteId { get; set; }
    public string Suggestion { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}




