using serenity.Application.DTOs;
using serenity.Application.Interfaces;
using serenity.Application.UseCases.MeditationSessions;

namespace serenity.Application.UseCases.MeditationSessions.Commands;

public class UpdateMeditationSessionUseCase
{
    private readonly IMeditationSessionRepository _sessionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateMeditationSessionUseCase(IMeditationSessionRepository sessionRepository, IUnitOfWork unitOfWork)
    {
        _sessionRepository = sessionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<MeditationSessionDto> ExecuteAsync(int id, UpdateMeditationSessionRequest request, CancellationToken cancellationToken = default)
    {
        var session = await _sessionRepository.GetByIdAsync(id, cancellationToken)
                     ?? throw new KeyNotFoundException($"No se encontró la sesión de meditación con id {id}.");

        if (request.SessionDate.HasValue)
        {
            session.SessionDate = request.SessionDate.Value;
        }

        if (request.DurationMinutes.HasValue)
        {
            session.DurationMinutes = request.DurationMinutes.Value;
        }

        if (request.Type is not null)
        {
            session.Type = request.Type;
        }

        if (request.Notes is not null)
        {
            session.Notes = request.Notes;
        }

        await _sessionRepository.UpdateAsync(session);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return session.ToDto();
    }
}




