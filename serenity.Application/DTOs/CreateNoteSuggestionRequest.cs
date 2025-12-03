namespace serenity.Application.DTOs;

public class CreateNoteSuggestionRequest
{
    public int NoteId { get; set; }
    public string Suggestion { get; set; } = string.Empty;
}



