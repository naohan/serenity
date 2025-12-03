using serenity.Application.DTOs;
using serenity.Infrastructure;

namespace serenity.Application.UseCases.MentalWellbeingMetrics;

internal static class MentalWellbeingMetricMappingExtensions
{
    public static MentalWellbeingMetricDto ToDto(this MentalWellbeingMetric metric)
    {
        return new MentalWellbeingMetricDto
        {
            Id = metric.Id,
            PatientId = metric.PatientId,
            Date = metric.Date,
            StressLevel = metric.StressLevel,
            EnergyLevel = metric.EnergyLevel,
            ConcentrationLevel = metric.ConcentrationLevel,
            SatisfactionLevel = metric.SatisfactionLevel,
            SleepDuration = metric.SleepDuration,
            SleepQuality = metric.SleepQuality,
            MeditationMinutes = metric.MeditationMinutes,
            MeditationSessions = metric.MeditationSessions,
            CreatedAt = metric.CreatedAt,
            UpdatedAt = metric.UpdatedAt
        };
    }
}




