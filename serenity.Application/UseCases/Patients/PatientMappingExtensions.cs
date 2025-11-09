using serenity.Application.DTOs;
using serenity.Infrastructure;

namespace serenity.Application.UseCases.Patients;

internal static class PatientMappingExtensions
{
    public static PatientDto ToDto(this Patient patient)
    {
        return new PatientDto
        {
            Id = patient.Id,
            UserId = patient.UserId,
            PsychologistId = patient.PsychologistId,
            Name = patient.Name,
            Age = patient.Age,
            Diagnosis = patient.Diagnosis,
            AvatarUrl = patient.AvatarUrl,
            Status = patient.Status,
            CreatedAt = patient.CreatedAt,
            UpdatedAt = patient.UpdatedAt
        };
    }
}

