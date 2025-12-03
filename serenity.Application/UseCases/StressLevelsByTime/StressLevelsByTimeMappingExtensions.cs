using serenity.Application.DTOs;
using serenity.Infrastructure;

namespace serenity.Application.UseCases.StressLevelsByTime;

internal static class StressLevelsByTimeMappingExtensions
{
    public static StressLevelsByTimeDto ToDto(this Infrastructure.StressLevelsByTime stressLevel)
    {
        return new StressLevelsByTimeDto
        {
            Id = stressLevel.Id,
            PatientId = stressLevel.PatientId,
            Date = stressLevel.Date,
            TimeOfDay = stressLevel.TimeOfDay,
            StressLevel = stressLevel.StressLevel,
            CreatedAt = stressLevel.CreatedAt
        };
    }
}

