using serenity.Application.DTOs;
using serenity.Application.Interfaces;
using serenity.Application.UseCases.EmotionalStates;

namespace serenity.Application.UseCases.EmotionalStates.Queries;

public class GetEmotionalStateByIdUseCase
{
    private readonly IEmotionalStateRepository _emotionalStateRepository;

    public GetEmotionalStateByIdUseCase(IEmotionalStateRepository emotionalStateRepository)
    {
        _emotionalStateRepository = emotionalStateRepository;
    }

    public async Task<EmotionalStateDto?> ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        var emotionalState = await _emotionalStateRepository.GetByIdAsync(id, cancellationToken);
        return emotionalState?.ToDto();
    }
}



