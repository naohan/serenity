using serenity.Application.DTOs;
using serenity.Application.Interfaces;
using serenity.Application.UseCases.EmotionalStates;

namespace serenity.Application.UseCases.EmotionalStates.Queries;

public class GetAllEmotionalStatesUseCase
{
    private readonly IEmotionalStateRepository _emotionalStateRepository;

    public GetAllEmotionalStatesUseCase(IEmotionalStateRepository emotionalStateRepository)
    {
        _emotionalStateRepository = emotionalStateRepository;
    }

    public async Task<IEnumerable<EmotionalStateDto>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var emotionalStates = await _emotionalStateRepository.GetAllAsync(cancellationToken);
        return emotionalStates.Select(e => e.ToDto());
    }
}



