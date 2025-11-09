namespace serenity.Application.DTOs;

public class PsychologistDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string? CollegeNumber { get; set; }
    public string? Country { get; set; }
    public string? Location { get; set; }
    public string? Specialization { get; set; }
    public int? WeeklyScore { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

