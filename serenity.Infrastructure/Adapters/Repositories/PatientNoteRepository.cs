using Microsoft.EntityFrameworkCore;
using serenity.Application.Interfaces;
using serenity.Infrastructure.Data;
using serenity.Infrastructure;

namespace serenity.Infrastructure.Adapters.Repositories;

public class PatientNoteRepository : Repository<PatientNote>, IPatientNoteRepository
{
    public PatientNoteRepository(SerenityDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<PatientNote>> GetByPatientIdAsync(int patientId, CancellationToken cancellationToken = default)
    {
        return await DbSet.Where(n => n.PatientId == patientId)
            .OrderByDescending(n => n.Date)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<PatientNote>> GetByPsychologistIdAsync(int psychologistId, CancellationToken cancellationToken = default)
    {
        return await DbSet.Where(n => n.PsychologistId == psychologistId)
            .OrderByDescending(n => n.Date)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<PatientNote>> GetByDateRangeAsync(int patientId, DateOnly startDate, DateOnly endDate, CancellationToken cancellationToken = default)
    {
        return await DbSet.Where(n => n.PatientId == patientId && n.Date >= startDate && n.Date <= endDate)
            .OrderBy(n => n.Date)
            .ToListAsync(cancellationToken);
    }
}



