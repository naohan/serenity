using serenity.Infrastructure;

namespace serenity.Domain.Ports.IRepositorios;

/// <summary>
/// Repository port for MentalWellbeingMetric domain entity.
/// </summary>
public interface IMentalWellbeingMetricRepository : IRepository<MentalWellbeingMetric>
{
    Task<MentalWellbeingMetric?> GetByPatientIdAndDateAsync(int patientId, DateOnly date, CancellationToken cancellationToken = default);
    Task<IEnumerable<MentalWellbeingMetric>> GetByPatientIdAsync(int patientId, CancellationToken cancellationToken = default);
    Task<IEnumerable<MentalWellbeingMetric>> GetByDateRangeAsync(int patientId, DateOnly startDate, DateOnly endDate, CancellationToken cancellationToken = default);
}




