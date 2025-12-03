using serenity.Application.DTOs;
using serenity.Application.Interfaces;
using serenity.Application.UseCases.EmotionalStates;

namespace serenity.Application.UseCases.EmotionalStates.Commands;

public class UpdateEmotionalStateUseCase
{
    private readonly IEmotionalStateRepository _emotionalStateRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateEmotionalStateUseCase(IEmotionalStateRepository emotionalStateRepository, IUnitOfWork unitOfWork)
    {
        _emotionalStateRepository = emotionalStateRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<EmotionalStateDto> ExecuteAsync(int id, UpdateEmotionalStateRequest request, CancellationToken cancellationToken = default)
    {
        var emotionalState = await _emotionalStateRepository.GetByIdAsync(id, cancellationToken)
                           ?? throw new KeyNotFoundException($"No se encontró el estado emocional con id {id}.");

        if (request.Date.HasValue)
        {
            emotionalState.Date = request.Date.Value;
        }

        if (request.EmotionalState1 is not null)
        {
            emotionalState.EmotionalState1 = request.EmotionalState1;
        }

        if (request.Value.HasValue)
        {
            emotionalState.Value = request.Value;
        }

        await _emotionalStateRepository.UpdateAsync(emotionalState);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return emotionalState.ToDto();
    }
}



