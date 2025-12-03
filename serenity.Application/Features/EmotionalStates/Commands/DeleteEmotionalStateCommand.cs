using MediatR;
using serenity.Application.UseCases.EmotionalStates.Commands;

namespace serenity.Application.Features.EmotionalStates.Commands;

public record DeleteEmotionalStateCommand(int Id) : IRequest;

public class DeleteEmotionalStateCommandHandler : IRequestHandler<DeleteEmotionalStateCommand>
{
    private readonly DeleteEmotionalStateUseCase _useCase;

    public DeleteEmotionalStateCommandHandler(DeleteEmotionalStateUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task Handle(DeleteEmotionalStateCommand request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(request.Id, cancellationToken);
    }
}




