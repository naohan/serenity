namespace serenity.Application.DTOs;

public class EmotionalStateDto
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public DateOnly Date { get; set; }
    public string? EmotionalState1 { get; set; }
    public decimal? Value { get; set; }
    public DateTime CreatedAt { get; set; }
}



