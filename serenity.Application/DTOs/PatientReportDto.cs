namespace serenity.Application.DTOs;

public class PatientReportDto
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public int PsychologistId { get; set; }
    public string? Title { get; set; }
    public string Diagnosis { get; set; } = string.Empty;
    public string? AnxietyLevel { get; set; }
    public string? Recommendations { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}



