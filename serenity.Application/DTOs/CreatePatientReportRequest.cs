namespace serenity.Application.DTOs;

public class CreatePatientReportRequest
{
    public int PatientId { get; set; }
    public int PsychologistId { get; set; }
    public string? Title { get; set; }
    public string Diagnosis { get; set; } = string.Empty;
    public string? AnxietyLevel { get; set; }
    public string? Recommendations { get; set; }
}




