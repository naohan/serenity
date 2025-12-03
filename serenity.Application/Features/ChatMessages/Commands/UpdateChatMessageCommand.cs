using MediatR;
using serenity.Application.DTOs;
using serenity.Application.UseCases.ChatMessages.Commands;

namespace serenity.Application.Features.ChatMessages.Commands;

public record UpdateChatMessageCommand(int Id, UpdateChatMessageRequest Request) : IRequest<ChatMessageDto>;

public class UpdateChatMessageCommandHandler : IRequestHandler<UpdateChatMessageCommand, ChatMessageDto>
{
    private readonly UpdateChatMessageUseCase _useCase;

    public UpdateChatMessageCommandHandler(UpdateChatMessageUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task<ChatMessageDto> Handle(UpdateChatMessageCommand request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(request.Id, request.Request, cancellationToken);
    }
}



