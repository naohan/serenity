using serenity.Application.DTOs;
using serenity.Application.Interfaces;
using serenity.Application.UseCases.MeditationSessions;

namespace serenity.Application.UseCases.MeditationSessions.Queries;

public class GetMeditationSessionByIdUseCase
{
    private readonly IMeditationSessionRepository _sessionRepository;

    public GetMeditationSessionByIdUseCase(IMeditationSessionRepository sessionRepository)
    {
        _sessionRepository = sessionRepository;
    }

    public async Task<MeditationSessionDto?> ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        var session = await _sessionRepository.GetByIdAsync(id, cancellationToken);
        return session?.ToDto();
    }
}



