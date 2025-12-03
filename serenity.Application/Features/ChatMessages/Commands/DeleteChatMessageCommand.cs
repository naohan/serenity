using MediatR;
using serenity.Application.UseCases.ChatMessages.Commands;

namespace serenity.Application.Features.ChatMessages.Commands;

public record DeleteChatMessageCommand(int Id) : IRequest;

public class DeleteChatMessageCommandHandler : IRequestHandler<DeleteChatMessageCommand>
{
    private readonly DeleteChatMessageUseCase _useCase;

    public DeleteChatMessageCommandHandler(DeleteChatMessageUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task Handle(DeleteChatMessageCommand request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(request.Id, cancellationToken);
    }
}



