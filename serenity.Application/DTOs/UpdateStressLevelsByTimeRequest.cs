namespace serenity.Application.DTOs;

public class UpdateStressLevelsByTimeRequest
{
    public DateOnly? Date { get; set; }
    public TimeOnly? TimeOfDay { get; set; }
    public sbyte? StressLevel { get; set; }
}



