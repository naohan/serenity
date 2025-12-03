namespace serenity.Application.DTOs;

public class UpdateDailyMoodRequest
{
    public DateOnly? Date { get; set; }
    public sbyte? Mood { get; set; }
    public string? Note { get; set; }
}



