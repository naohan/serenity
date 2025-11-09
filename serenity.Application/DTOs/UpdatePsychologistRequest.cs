namespace serenity.Application.DTOs;

public class UpdatePsychologistRequest
{
    public string? Country { get; set; }
    public string? Location { get; set; }
    public string? Specialization { get; set; }
    public int? WeeklyScore { get; set; }
}

