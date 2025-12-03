using Microsoft.EntityFrameworkCore;
using serenity.Application.Interfaces;
using serenity.Infrastructure.Data;
using serenity.Infrastructure;

namespace serenity.Infrastructure.Adapters.Repositories;

public class StressLevelsByTimeRepository : Repository<Infrastructure.StressLevelsByTime>, IStressLevelsByTimeRepository
{
    public StressLevelsByTimeRepository(SerenityDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Infrastructure.StressLevelsByTime>> GetByPatientIdAsync(int patientId, CancellationToken cancellationToken = default)
    {
        return await DbSet.Where(s => s.PatientId == patientId)
            .OrderByDescending(s => s.Date)
            .ThenBy(s => s.TimeOfDay)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Infrastructure.StressLevelsByTime>> GetByDateAsync(int patientId, DateOnly date, CancellationToken cancellationToken = default)
    {
        return await DbSet.Where(s => s.PatientId == patientId && s.Date == date)
            .OrderBy(s => s.TimeOfDay)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Infrastructure.StressLevelsByTime>> GetByDateRangeAsync(int patientId, DateOnly startDate, DateOnly endDate, CancellationToken cancellationToken = default)
    {
        return await DbSet.Where(s => s.PatientId == patientId && s.Date >= startDate && s.Date <= endDate)
            .OrderBy(s => s.Date)
            .ThenBy(s => s.TimeOfDay)
            .ToListAsync(cancellationToken);
    }
}

