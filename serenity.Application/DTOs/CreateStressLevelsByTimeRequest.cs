namespace serenity.Application.DTOs;

public class CreateStressLevelsByTimeRequest
{
    public int PatientId { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly TimeOfDay { get; set; }
    public sbyte StressLevel { get; set; }
}



