using MediatR;
using serenity.Application.DTOs;
using serenity.Application.UseCases.ChatMessages.Queries;

namespace serenity.Application.Features.ChatMessages.Queries;

public record GetAllChatMessagesQuery : IRequest<IEnumerable<ChatMessageDto>>;

public class GetAllChatMessagesQueryHandler : IRequestHandler<GetAllChatMessagesQuery, IEnumerable<ChatMessageDto>>
{
    private readonly GetAllChatMessagesUseCase _useCase;

    public GetAllChatMessagesQueryHandler(GetAllChatMessagesUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task<IEnumerable<ChatMessageDto>> Handle(GetAllChatMessagesQuery request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(cancellationToken);
    }
}




