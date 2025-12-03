namespace serenity.Application.DTOs;

public class CreateDailyMoodRequest
{
    public int PatientId { get; set; }
    public DateOnly Date { get; set; }
    public sbyte Mood { get; set; }
    public string? Note { get; set; }
}




