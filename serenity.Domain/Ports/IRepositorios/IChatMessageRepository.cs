using serenity.Infrastructure;

namespace serenity.Domain.Ports.IRepositorios;

/// <summary>
/// Repository port for ChatMessage domain entity.
/// </summary>
public interface IChatMessageRepository : IRepository<ChatMessage>
{
    Task<IEnumerable<ChatMessage>> GetByPatientIdAsync(int patientId, CancellationToken cancellationToken = default);
    Task<IEnumerable<ChatMessage>> GetByPsychologistIdAsync(int psychologistId, CancellationToken cancellationToken = default);
    Task<IEnumerable<ChatMessage>> GetConversationAsync(int patientId, int psychologistId, CancellationToken cancellationToken = default);
    Task<int> GetUnreadCountAsync(int patientId, int psychologistId, bool isFromPsychologist, CancellationToken cancellationToken = default);
}



