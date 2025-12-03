using MediatR;
using serenity.Application.DTOs;
using serenity.Application.UseCases.EmotionalStates.Commands;

namespace serenity.Application.Features.EmotionalStates.Commands;

public record UpdateEmotionalStateCommand(int Id, UpdateEmotionalStateRequest Request) : IRequest<EmotionalStateDto>;

public class UpdateEmotionalStateCommandHandler : IRequestHandler<UpdateEmotionalStateCommand, EmotionalStateDto>
{
    private readonly UpdateEmotionalStateUseCase _useCase;

    public UpdateEmotionalStateCommandHandler(UpdateEmotionalStateUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task<EmotionalStateDto> Handle(UpdateEmotionalStateCommand request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(request.Id, request.Request, cancellationToken);
    }
}




