using serenity.Application.DTOs;
using serenity.Infrastructure;

namespace serenity.Application.UseCases.PatientReports;

internal static class PatientReportMappingExtensions
{
    public static PatientReportDto ToDto(this PatientReport report)
    {
        return new PatientReportDto
        {
            Id = report.Id,
            PatientId = report.PatientId,
            PsychologistId = report.PsychologistId,
            Title = report.Title,
            Diagnosis = report.Diagnosis,
            AnxietyLevel = report.AnxietyLevel,
            Recommendations = report.Recommendations,
            CreatedAt = report.CreatedAt,
            UpdatedAt = report.UpdatedAt
        };
    }
}



