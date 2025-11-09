namespace serenity.Application.DTOs;

public class CreateTrainerRequest
{
    public int UserId { get; set; }
    public string? Bio { get; set; }
    public string? Specialization { get; set; }
}

