using serenity.Application.DTOs;
using serenity.Application.Interfaces;
using serenity.Application.UseCases.ChatMessages;

namespace serenity.Application.UseCases.ChatMessages.Queries;

public class GetChatMessageByIdUseCase
{
    private readonly IChatMessageRepository _chatMessageRepository;

    public GetChatMessageByIdUseCase(IChatMessageRepository chatMessageRepository)
    {
        _chatMessageRepository = chatMessageRepository;
    }

    public async Task<ChatMessageDto?> ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        var chatMessage = await _chatMessageRepository.GetByIdAsync(id, cancellationToken);
        return chatMessage?.ToDto();
    }
}




