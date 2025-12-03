namespace serenity.Application.DTOs;

public class UpdateEmotionalStateRequest
{
    public DateOnly? Date { get; set; }
    public string? EmotionalState1 { get; set; }
    public decimal? Value { get; set; }
}



