using serenity.Application.DTOs;
using serenity.Application.Interfaces;
using serenity.Application.UseCases.ChatMessages;
using serenity.Infrastructure;

namespace serenity.Application.UseCases.ChatMessages.Commands;

public class CreateChatMessageUseCase
{
    private readonly IChatMessageRepository _chatMessageRepository;
    private readonly IPatientRepository _patientRepository;
    private readonly IPsychologistRepository _psychologistRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateChatMessageUseCase(
        IChatMessageRepository chatMessageRepository,
        IPatientRepository patientRepository,
        IPsychologistRepository psychologistRepository,
        IUnitOfWork unitOfWork)
    {
        _chatMessageRepository = chatMessageRepository;
        _patientRepository = patientRepository;
        _psychologistRepository = psychologistRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ChatMessageDto> ExecuteAsync(CreateChatMessageRequest request, CancellationToken cancellationToken = default)
    {
        var patient = await _patientRepository.GetByIdAsync(request.PatientId, cancellationToken);
        if (patient is null)
        {
            throw new KeyNotFoundException($"No se encontró el paciente con id {request.PatientId}.");
        }

        var psychologist = await _psychologistRepository.GetByIdAsync(request.PsychologistId, cancellationToken);
        if (psychologist is null)
        {
            throw new KeyNotFoundException($"No se encontró el psicólogo con id {request.PsychologistId}.");
        }

        var chatMessage = new ChatMessage
        {
            PatientId = request.PatientId,
            PsychologistId = request.PsychologistId,
            MessageText = request.MessageText,
            IsFromPsychologist = request.IsFromPsychologist,
            SentAt = DateTime.Now,
            ReadAt = null
        };

        await _chatMessageRepository.AddAsync(chatMessage, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return chatMessage.ToDto();
    }
}




