using serenity.Infrastructure;

namespace serenity.Application.Interfaces;

public interface IMoodMetricRepository : IRepository<MoodMetric>
{
    Task<MoodMetric?> GetByPatientIdAndDateAsync(int patientId, DateOnly date, CancellationToken cancellationToken = default);
    Task<IEnumerable<MoodMetric>> GetByPatientIdAsync(int patientId, CancellationToken cancellationToken = default);
    Task<IEnumerable<MoodMetric>> GetByDateRangeAsync(int patientId, DateOnly startDate, DateOnly endDate, CancellationToken cancellationToken = default);
}




