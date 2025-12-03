using serenity.Application.DTOs;
using serenity.Infrastructure;

namespace serenity.Application.UseCases.EmotionalStates;

internal static class EmotionalStateMappingExtensions
{
    public static EmotionalStateDto ToDto(this EmotionalState emotionalState)
    {
        return new EmotionalStateDto
        {
            Id = emotionalState.Id,
            PatientId = emotionalState.PatientId,
            Date = emotionalState.Date,
            EmotionalState1 = emotionalState.EmotionalState1,
            Value = emotionalState.Value,
            CreatedAt = emotionalState.CreatedAt
        };
    }
}




