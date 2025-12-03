using Microsoft.EntityFrameworkCore;
using serenity.Application.Interfaces;
using serenity.Infrastructure.Data;
using serenity.Infrastructure;

namespace serenity.Infrastructure.Adapters.Repositories;

public class PrescriptionRepository : Repository<Prescription>, IPrescriptionRepository
{
    public PrescriptionRepository(SerenityDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Prescription>> GetByPatientIdAsync(int patientId, CancellationToken cancellationToken = default)
    {
        return await DbSet.Where(p => p.PatientId == patientId)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Prescription>> GetByPsychologistIdAsync(int psychologistId, CancellationToken cancellationToken = default)
    {
        return await DbSet.Where(p => p.PsychologistId == psychologistId)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Prescription>> GetActiveByPatientIdAsync(int patientId, CancellationToken cancellationToken = default)
    {
        var today = DateOnly.FromDateTime(DateTime.Now);
        return await DbSet.Where(p => p.PatientId == patientId 
            && p.StartDate <= today 
            && (p.EndDate == null || p.EndDate >= today)
            && (p.Status == null || p.Status.ToLower() != "cancelled"))
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync(cancellationToken);
    }
}




