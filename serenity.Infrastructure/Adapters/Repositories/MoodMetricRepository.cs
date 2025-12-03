using Microsoft.EntityFrameworkCore;
using serenity.Application.Interfaces;
using serenity.Infrastructure.Data;
using serenity.Infrastructure;

namespace serenity.Infrastructure.Adapters.Repositories;

public class MoodMetricRepository : Repository<MoodMetric>, IMoodMetricRepository
{
    public MoodMetricRepository(SerenityDbContext context) : base(context)
    {
    }

    public async Task<MoodMetric?> GetByPatientIdAndDateAsync(int patientId, DateOnly date, CancellationToken cancellationToken = default)
    {
        return await DbSet.FirstOrDefaultAsync(m => m.PatientId == patientId && m.Date == date, cancellationToken);
    }

    public async Task<IEnumerable<MoodMetric>> GetByPatientIdAsync(int patientId, CancellationToken cancellationToken = default)
    {
        return await DbSet.Where(m => m.PatientId == patientId)
            .OrderByDescending(m => m.Date)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<MoodMetric>> GetByDateRangeAsync(int patientId, DateOnly startDate, DateOnly endDate, CancellationToken cancellationToken = default)
    {
        return await DbSet.Where(m => m.PatientId == patientId && m.Date >= startDate && m.Date <= endDate)
            .OrderBy(m => m.Date)
            .ToListAsync(cancellationToken);
    }
}



