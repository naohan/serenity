using serenity.Application.DTOs;
using serenity.Infrastructure;

namespace serenity.Application.UseCases.DailyMoods;

internal static class DailyMoodMappingExtensions
{
    public static DailyMoodDto ToDto(this DailyMood dailyMood)
    {
        return new DailyMoodDto
        {
            Id = dailyMood.Id,
            PatientId = dailyMood.PatientId,
            Date = dailyMood.Date,
            Mood = dailyMood.Mood,
            Note = dailyMood.Note,
            CreatedAt = dailyMood.CreatedAt
        };
    }
}



