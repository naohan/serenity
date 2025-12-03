using serenity.Application.DTOs;
using serenity.Infrastructure;

namespace serenity.Application.UseCases.MeditationSessions;

internal static class MeditationSessionMappingExtensions
{
    public static MeditationSessionDto ToDto(this MeditationSession session)
    {
        return new MeditationSessionDto
        {
            Id = session.Id,
            PatientId = session.PatientId,
            SessionDate = session.SessionDate,
            DurationMinutes = session.DurationMinutes,
            Type = session.Type,
            Notes = session.Notes,
            CreatedAt = session.CreatedAt
        };
    }
}




