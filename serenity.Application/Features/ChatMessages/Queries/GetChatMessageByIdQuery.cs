using MediatR;
using serenity.Application.DTOs;
using serenity.Application.UseCases.ChatMessages.Queries;

namespace serenity.Application.Features.ChatMessages.Queries;

public record GetChatMessageByIdQuery(int Id) : IRequest<ChatMessageDto?>;

public class GetChatMessageByIdQueryHandler : IRequestHandler<GetChatMessageByIdQuery, ChatMessageDto?>
{
    private readonly GetChatMessageByIdUseCase _useCase;

    public GetChatMessageByIdQueryHandler(GetChatMessageByIdUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task<ChatMessageDto?> Handle(GetChatMessageByIdQuery request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(request.Id, cancellationToken);
    }
}



