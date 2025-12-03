using MediatR;
using serenity.Application.DTOs;
using serenity.Application.UseCases.EmotionalStates.Commands;

namespace serenity.Application.Features.EmotionalStates.Commands;

public record CreateEmotionalStateCommand(CreateEmotionalStateRequest Request) : IRequest<EmotionalStateDto>;

public class CreateEmotionalStateCommandHandler : IRequestHandler<CreateEmotionalStateCommand, EmotionalStateDto>
{
    private readonly CreateEmotionalStateUseCase _useCase;

    public CreateEmotionalStateCommandHandler(CreateEmotionalStateUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task<EmotionalStateDto> Handle(CreateEmotionalStateCommand request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(request.Request, cancellationToken);
    }
}




