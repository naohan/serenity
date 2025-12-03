using Microsoft.EntityFrameworkCore;
using serenity.Application.Interfaces;
using serenity.Infrastructure.Data;
using serenity.Infrastructure;

namespace serenity.Infrastructure.Adapters.Repositories;

public class InsightsAndRecommendationRepository : Repository<InsightsAndRecommendation>, IInsightsAndRecommendationRepository
{
    public InsightsAndRecommendationRepository(SerenityDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<InsightsAndRecommendation>> GetByPatientIdAsync(int patientId, CancellationToken cancellationToken = default)
    {
        return await DbSet.Where(i => i.PatientId == patientId)
            .OrderByDescending(i => i.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<InsightsAndRecommendation>> GetByPsychologistIdAsync(int psychologistId, CancellationToken cancellationToken = default)
    {
        return await DbSet.Where(i => i.PsychologistId == psychologistId)
            .OrderByDescending(i => i.CreatedAt)
            .ToListAsync(cancellationToken);
    }
}



