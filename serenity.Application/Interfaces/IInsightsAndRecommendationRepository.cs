using serenity.Infrastructure;

namespace serenity.Application.Interfaces;

public interface IInsightsAndRecommendationRepository : IRepository<InsightsAndRecommendation>
{
    Task<IEnumerable<InsightsAndRecommendation>> GetByPatientIdAsync(int patientId, CancellationToken cancellationToken = default);
    Task<IEnumerable<InsightsAndRecommendation>> GetByPsychologistIdAsync(int psychologistId, CancellationToken cancellationToken = default);
}




