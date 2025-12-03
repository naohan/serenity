using serenity.Infrastructure;

namespace serenity.Domain.Ports.IRepositorios;

/// <summary>
/// Repository port for InsightsAndRecommendation domain entity.
/// </summary>
public interface IInsightsAndRecommendationRepository : IRepository<InsightsAndRecommendation>
{
    Task<IEnumerable<InsightsAndRecommendation>> GetByPatientIdAsync(int patientId, CancellationToken cancellationToken = default);
    Task<IEnumerable<InsightsAndRecommendation>> GetByPsychologistIdAsync(int psychologistId, CancellationToken cancellationToken = default);
}




