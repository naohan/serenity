using Microsoft.EntityFrameworkCore;
using serenity.Application.Interfaces;
using serenity.Infrastructure.Data;
using serenity.Infrastructure;

namespace serenity.Infrastructure.Adapters.Repositories;

public class EmotionalStateRepository : Repository<EmotionalState>, IEmotionalStateRepository
{
    public EmotionalStateRepository(SerenityDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<EmotionalState>> GetByPatientIdAsync(int patientId, CancellationToken cancellationToken = default)
    {
        return await DbSet.Where(e => e.PatientId == patientId)
            .OrderByDescending(e => e.Date)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<EmotionalState>> GetByDateRangeAsync(int patientId, DateOnly startDate, DateOnly endDate, CancellationToken cancellationToken = default)
    {
        return await DbSet.Where(e => e.PatientId == patientId && e.Date >= startDate && e.Date <= endDate)
            .OrderBy(e => e.Date)
            .ToListAsync(cancellationToken);
    }
}



