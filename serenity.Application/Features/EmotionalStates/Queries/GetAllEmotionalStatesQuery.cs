using MediatR;
using serenity.Application.DTOs;
using serenity.Application.UseCases.EmotionalStates.Queries;

namespace serenity.Application.Features.EmotionalStates.Queries;

public record GetAllEmotionalStatesQuery : IRequest<IEnumerable<EmotionalStateDto>>;

public class GetAllEmotionalStatesQueryHandler : IRequestHandler<GetAllEmotionalStatesQuery, IEnumerable<EmotionalStateDto>>
{
    private readonly GetAllEmotionalStatesUseCase _useCase;

    public GetAllEmotionalStatesQueryHandler(GetAllEmotionalStatesUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task<IEnumerable<EmotionalStateDto>> Handle(GetAllEmotionalStatesQuery request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(cancellationToken);
    }
}



