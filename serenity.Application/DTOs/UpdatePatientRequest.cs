namespace serenity.Application.DTOs;

public class UpdatePatientRequest
{
    public int? PsychologistId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int? Age { get; set; }
    public string? Diagnosis { get; set; }
    public string? AvatarUrl { get; set; }
    public string? Status { get; set; }
}

