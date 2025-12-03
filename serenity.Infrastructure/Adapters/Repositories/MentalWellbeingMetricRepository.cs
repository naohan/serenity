using Microsoft.EntityFrameworkCore;
using serenity.Application.Interfaces;
using serenity.Infrastructure.Data;
using serenity.Infrastructure;

namespace serenity.Infrastructure.Adapters.Repositories;

public class MentalWellbeingMetricRepository : Repository<MentalWellbeingMetric>, IMentalWellbeingMetricRepository
{
    public MentalWellbeingMetricRepository(SerenityDbContext context) : base(context)
    {
    }

    public async Task<MentalWellbeingMetric?> GetByPatientIdAndDateAsync(int patientId, DateOnly date, CancellationToken cancellationToken = default)
    {
        return await DbSet.FirstOrDefaultAsync(m => m.PatientId == patientId && m.Date == date, cancellationToken);
    }

    public async Task<IEnumerable<MentalWellbeingMetric>> GetByPatientIdAsync(int patientId, CancellationToken cancellationToken = default)
    {
        return await DbSet.Where(m => m.PatientId == patientId)
            .OrderByDescending(m => m.Date)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<MentalWellbeingMetric>> GetByDateRangeAsync(int patientId, DateOnly startDate, DateOnly endDate, CancellationToken cancellationToken = default)
    {
        return await DbSet.Where(m => m.PatientId == patientId && m.Date >= startDate && m.Date <= endDate)
            .OrderBy(m => m.Date)
            .ToListAsync(cancellationToken);
    }
}



