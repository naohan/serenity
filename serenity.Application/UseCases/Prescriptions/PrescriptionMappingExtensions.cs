using serenity.Application.DTOs;
using serenity.Infrastructure;

namespace serenity.Application.UseCases.Prescriptions;

internal static class PrescriptionMappingExtensions
{
    public static PrescriptionDto ToDto(this Prescription prescription)
    {
        return new PrescriptionDto
        {
            Id = prescription.Id,
            PatientId = prescription.PatientId,
            PsychologistId = prescription.PsychologistId,
            MedicationName = prescription.MedicationName,
            Dosage = prescription.Dosage,
            Frequency = prescription.Frequency,
            StartDate = prescription.StartDate,
            EndDate = prescription.EndDate,
            Instructions = prescription.Instructions,
            Status = prescription.Status,
            CreatedAt = prescription.CreatedAt,
            UpdatedAt = prescription.UpdatedAt
        };
    }
}



