using MediatR;
using serenity.Application.DTOs;
using serenity.Application.UseCases.EmotionalStates.Queries;

namespace serenity.Application.Features.EmotionalStates.Queries;

public record GetEmotionalStateByIdQuery(int Id) : IRequest<EmotionalStateDto?>;

public class GetEmotionalStateByIdQueryHandler : IRequestHandler<GetEmotionalStateByIdQuery, EmotionalStateDto?>
{
    private readonly GetEmotionalStateByIdUseCase _useCase;

    public GetEmotionalStateByIdQueryHandler(GetEmotionalStateByIdUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task<EmotionalStateDto?> Handle(GetEmotionalStateByIdQuery request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(request.Id, cancellationToken);
    }
}




