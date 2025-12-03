using serenity.Application.DTOs;
using serenity.Infrastructure;

namespace serenity.Application.UseCases.InsightsAndRecommendations;

internal static class InsightsAndRecommendationMappingExtensions
{
    public static InsightsAndRecommendationDto ToDto(this InsightsAndRecommendation insight)
    {
        return new InsightsAndRecommendationDto
        {
            Id = insight.Id,
            PatientId = insight.PatientId,
            PsychologistId = insight.PsychologistId,
            Title = insight.Title,
            Description = insight.Description,
            Type = insight.Type,
            Priority = insight.Priority,
            CreatedAt = insight.CreatedAt,
            UpdatedAt = insight.UpdatedAt
        };
    }
}




