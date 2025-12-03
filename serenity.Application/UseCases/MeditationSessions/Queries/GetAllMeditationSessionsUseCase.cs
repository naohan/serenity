using serenity.Application.DTOs;
using serenity.Application.Interfaces;
using serenity.Application.UseCases.MeditationSessions;

namespace serenity.Application.UseCases.MeditationSessions.Queries;

public class GetAllMeditationSessionsUseCase
{
    private readonly IMeditationSessionRepository _sessionRepository;

    public GetAllMeditationSessionsUseCase(IMeditationSessionRepository sessionRepository)
    {
        _sessionRepository = sessionRepository;
    }

    public async Task<IEnumerable<MeditationSessionDto>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var sessions = await _sessionRepository.GetAllAsync(cancellationToken);
        return sessions.Select(s => s.ToDto());
    }
}



