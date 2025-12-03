namespace serenity.Application.DTOs;

public class DailyMoodDto
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public DateOnly Date { get; set; }
    public sbyte Mood { get; set; }
    public string? Note { get; set; }
    public DateTime CreatedAt { get; set; }
}




