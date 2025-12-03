using MediatR;
using serenity.Application.DTOs;
using serenity.Application.UseCases.ChatMessages.Commands;

namespace serenity.Application.Features.ChatMessages.Commands;

public record CreateChatMessageCommand(CreateChatMessageRequest Request) : IRequest<ChatMessageDto>;

public class CreateChatMessageCommandHandler : IRequestHandler<CreateChatMessageCommand, ChatMessageDto>
{
    private readonly CreateChatMessageUseCase _useCase;

    public CreateChatMessageCommandHandler(CreateChatMessageUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task<ChatMessageDto> Handle(CreateChatMessageCommand request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(request.Request, cancellationToken);
    }
}




