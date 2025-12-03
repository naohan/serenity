using serenity.Application.Interfaces;

namespace serenity.Application.UseCases.MeditationSessions.Commands;

public class DeleteMeditationSessionUseCase
{
    private readonly IMeditationSessionRepository _sessionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteMeditationSessionUseCase(IMeditationSessionRepository sessionRepository, IUnitOfWork unitOfWork)
    {
        _sessionRepository = sessionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        var session = await _sessionRepository.GetByIdAsync(id, cancellationToken)
                    ?? throw new KeyNotFoundException($"No se encontró la sesión de meditación con id {id}.");

        await _sessionRepository.DeleteAsync(session);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}



