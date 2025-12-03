using serenity.Application.DTOs;
using serenity.Application.Interfaces;
using serenity.Application.UseCases.ChatMessages;

namespace serenity.Application.UseCases.ChatMessages.Commands;

public class UpdateChatMessageUseCase
{
    private readonly IChatMessageRepository _chatMessageRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateChatMessageUseCase(IChatMessageRepository chatMessageRepository, IUnitOfWork unitOfWork)
    {
        _chatMessageRepository = chatMessageRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ChatMessageDto> ExecuteAsync(int id, UpdateChatMessageRequest request, CancellationToken cancellationToken = default)
    {
        var chatMessage = await _chatMessageRepository.GetByIdAsync(id, cancellationToken)
                         ?? throw new KeyNotFoundException($"No se encontró el mensaje con id {id}.");

        if (request.MessageText is not null)
        {
            chatMessage.MessageText = request.MessageText;
        }

        if (request.IsFromPsychologist.HasValue)
        {
            chatMessage.IsFromPsychologist = request.IsFromPsychologist.Value;
        }

        if (request.ReadAt.HasValue)
        {
            chatMessage.ReadAt = request.ReadAt.Value;
        }

        await _chatMessageRepository.UpdateAsync(chatMessage);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return chatMessage.ToDto();
    }
}



