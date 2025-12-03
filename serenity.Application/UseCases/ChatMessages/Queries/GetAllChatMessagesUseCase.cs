using serenity.Application.DTOs;
using serenity.Application.Interfaces;
using serenity.Application.UseCases.ChatMessages;

namespace serenity.Application.UseCases.ChatMessages.Queries;

public class GetAllChatMessagesUseCase
{
    private readonly IChatMessageRepository _chatMessageRepository;

    public GetAllChatMessagesUseCase(IChatMessageRepository chatMessageRepository)
    {
        _chatMessageRepository = chatMessageRepository;
    }

    public async Task<IEnumerable<ChatMessageDto>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var chatMessages = await _chatMessageRepository.GetAllAsync(cancellationToken);
        return chatMessages.Select(m => m.ToDto());
    }
}




