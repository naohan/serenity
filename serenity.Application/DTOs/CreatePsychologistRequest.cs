namespace serenity.Application.DTOs;

public class CreatePsychologistRequest
{
    public int UserId { get; set; }
    public string CollegeNumber { get; set; } = string.Empty;
    public string? Country { get; set; }
    public string? Location { get; set; }
    public string? Specialization { get; set; }
    public int? WeeklyScore { get; set; }
}

