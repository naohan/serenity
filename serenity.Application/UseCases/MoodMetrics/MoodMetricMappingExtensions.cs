using serenity.Application.DTOs;
using serenity.Infrastructure;

namespace serenity.Application.UseCases.MoodMetrics;

internal static class MoodMetricMappingExtensions
{
    public static MoodMetricDto ToDto(this MoodMetric moodMetric)
    {
        return new MoodMetricDto
        {
            Id = moodMetric.Id,
            PatientId = moodMetric.PatientId,
            Date = moodMetric.Date,
            HappyPercentage = moodMetric.HappyPercentage,
            CalmPercentage = moodMetric.CalmPercentage,
            SadPercentage = moodMetric.SadPercentage,
            AnxiousPercentage = moodMetric.AnxiousPercentage,
            CreatedAt = moodMetric.CreatedAt,
            UpdatedAt = moodMetric.UpdatedAt
        };
    }
}




