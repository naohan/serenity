using serenity.Application.DTOs;
using serenity.Infrastructure;

namespace serenity.Application.UseCases.ChatMessages;

internal static class ChatMessageMappingExtensions
{
    public static ChatMessageDto ToDto(this ChatMessage chatMessage)
    {
        return new ChatMessageDto
        {
            Id = chatMessage.Id,
            PatientId = chatMessage.PatientId,
            PsychologistId = chatMessage.PsychologistId,
            MessageText = chatMessage.MessageText,
            IsFromPsychologist = chatMessage.IsFromPsychologist,
            SentAt = chatMessage.SentAt,
            ReadAt = chatMessage.ReadAt
        };
    }
}




