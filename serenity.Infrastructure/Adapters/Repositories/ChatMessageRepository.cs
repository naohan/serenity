using Microsoft.EntityFrameworkCore;
using serenity.Application.Interfaces;
using serenity.Infrastructure.Data;
using serenity.Infrastructure;

namespace serenity.Infrastructure.Adapters.Repositories;

public class ChatMessageRepository : Repository<ChatMessage>, IChatMessageRepository
{
    public ChatMessageRepository(SerenityDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<ChatMessage>> GetByPatientIdAsync(int patientId, CancellationToken cancellationToken = default)
    {
        return await DbSet.Where(m => m.PatientId == patientId)
            .OrderBy(m => m.SentAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<ChatMessage>> GetByPsychologistIdAsync(int psychologistId, CancellationToken cancellationToken = default)
    {
        return await DbSet.Where(m => m.PsychologistId == psychologistId)
            .OrderBy(m => m.SentAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<ChatMessage>> GetConversationAsync(int patientId, int psychologistId, CancellationToken cancellationToken = default)
    {
        return await DbSet.Where(m => m.PatientId == patientId && m.PsychologistId == psychologistId)
            .OrderBy(m => m.SentAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> GetUnreadCountAsync(int patientId, int psychologistId, bool isFromPsychologist, CancellationToken cancellationToken = default)
    {
        return await DbSet.CountAsync(m => 
            m.PatientId == patientId && 
            m.PsychologistId == psychologistId && 
            m.IsFromPsychologist == isFromPsychologist && 
            m.ReadAt == null, 
            cancellationToken);
    }
}




