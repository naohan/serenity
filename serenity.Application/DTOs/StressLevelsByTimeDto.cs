namespace serenity.Application.DTOs;

public class StressLevelsByTimeDto
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly TimeOfDay { get; set; }
    public sbyte StressLevel { get; set; }
    public DateTime CreatedAt { get; set; }
}




