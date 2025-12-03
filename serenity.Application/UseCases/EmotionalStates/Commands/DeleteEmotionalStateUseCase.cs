using serenity.Application.Interfaces;

namespace serenity.Application.UseCases.EmotionalStates.Commands;

public class DeleteEmotionalStateUseCase
{
    private readonly IEmotionalStateRepository _emotionalStateRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteEmotionalStateUseCase(IEmotionalStateRepository emotionalStateRepository, IUnitOfWork unitOfWork)
    {
        _emotionalStateRepository = emotionalStateRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        var emotionalState = await _emotionalStateRepository.GetByIdAsync(id, cancellationToken)
                           ?? throw new KeyNotFoundException($"No se encontró el estado emocional con id {id}.");

        await _emotionalStateRepository.DeleteAsync(emotionalState);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}



