using Microsoft.EntityFrameworkCore;
using serenity.Application.Interfaces;
using serenity.Infrastructure.Data;
using serenity.Infrastructure;

namespace serenity.Infrastructure.Adapters.Repositories;

public class DailyMoodRepository : Repository<DailyMood>, IDailyMoodRepository
{
    public DailyMoodRepository(SerenityDbContext context) : base(context)
    {
    }

    public async Task<DailyMood?> GetByPatientIdAndDateAsync(int patientId, DateOnly date, CancellationToken cancellationToken = default)
    {
        return await DbSet.FirstOrDefaultAsync(d => d.PatientId == patientId && d.Date == date, cancellationToken);
    }

    public async Task<IEnumerable<DailyMood>> GetByPatientIdAsync(int patientId, CancellationToken cancellationToken = default)
    {
        return await DbSet.Where(d => d.PatientId == patientId)
            .OrderByDescending(d => d.Date)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<DailyMood>> GetByDateRangeAsync(int patientId, DateOnly startDate, DateOnly endDate, CancellationToken cancellationToken = default)
    {
        return await DbSet.Where(d => d.PatientId == patientId && d.Date >= startDate && d.Date <= endDate)
            .OrderBy(d => d.Date)
            .ToListAsync(cancellationToken);
    }
}




