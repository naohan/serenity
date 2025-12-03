namespace serenity.Application.DTOs;

public class CreatePrescriptionRequest
{
    public int PatientId { get; set; }
    public int PsychologistId { get; set; }
    public string MedicationName { get; set; } = string.Empty;
    public string? Dosage { get; set; }
    public string? Frequency { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public string? Instructions { get; set; }
    public string? Status { get; set; }
}




