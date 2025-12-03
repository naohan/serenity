using serenity.Application.Interfaces;

namespace serenity.Application.UseCases.ChatMessages.Commands;

public class DeleteChatMessageUseCase
{
    private readonly IChatMessageRepository _chatMessageRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteChatMessageUseCase(IChatMessageRepository chatMessageRepository, IUnitOfWork unitOfWork)
    {
        _chatMessageRepository = chatMessageRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        var chatMessage = await _chatMessageRepository.GetByIdAsync(id, cancellationToken)
                         ?? throw new KeyNotFoundException($"No se encontró el mensaje con id {id}.");

        await _chatMessageRepository.DeleteAsync(chatMessage);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}



