using serenity.Application.DTOs;
using serenity.Infrastructure;

namespace serenity.Application.UseCases.Psychologists;

internal static class PsychologistMappingExtensions
{
    public static PsychologistDto ToDto(this Psychologist psychologist)
    {
        return new PsychologistDto
        {
            Id = psychologist.Id,
            UserId = psychologist.UserId,
            CollegeNumber = psychologist.CollegeNumber,
            Country = psychologist.Country,
            Location = psychologist.Location,
            Specialization = psychologist.Specialization,
            WeeklyScore = psychologist.WeeklyScore,
            CreatedAt = psychologist.CreatedAt,
            UpdatedAt = psychologist.UpdatedAt
        };
    }
}

