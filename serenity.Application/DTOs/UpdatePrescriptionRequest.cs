namespace serenity.Application.DTOs;

public class UpdatePrescriptionRequest
{
    public string? MedicationName { get; set; }
    public string? Dosage { get; set; }
    public string? Frequency { get; set; }
    public DateOnly? StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public string? Instructions { get; set; }
    public string? Status { get; set; }
}




