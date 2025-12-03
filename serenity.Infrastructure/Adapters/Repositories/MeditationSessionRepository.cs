using Microsoft.EntityFrameworkCore;
using serenity.Application.Interfaces;
using serenity.Infrastructure.Data;
using serenity.Infrastructure;

namespace serenity.Infrastructure.Adapters.Repositories;

public class MeditationSessionRepository : Repository<MeditationSession>, IMeditationSessionRepository
{
    public MeditationSessionRepository(SerenityDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<MeditationSession>> GetByPatientIdAsync(int patientId, CancellationToken cancellationToken = default)
    {
        return await DbSet.Where(m => m.PatientId == patientId)
            .OrderByDescending(m => m.SessionDate)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<MeditationSession>> GetByDateRangeAsync(int patientId, DateOnly startDate, DateOnly endDate, CancellationToken cancellationToken = default)
    {
        return await DbSet.Where(m => m.PatientId == patientId && m.SessionDate >= startDate && m.SessionDate <= endDate)
            .OrderBy(m => m.SessionDate)
            .ToListAsync(cancellationToken);
    }
}




