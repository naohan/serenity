namespace serenity.Application.DTOs;

public class CreateEmotionalStateRequest
{
    public int PatientId { get; set; }
    public DateOnly Date { get; set; }
    public string? EmotionalState1 { get; set; }
    public decimal? Value { get; set; }
}



