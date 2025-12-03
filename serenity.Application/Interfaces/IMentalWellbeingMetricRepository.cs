using serenity.Infrastructure;

namespace serenity.Application.Interfaces;

public interface IMentalWellbeingMetricRepository : IRepository<MentalWellbeingMetric>
{
    Task<MentalWellbeingMetric?> GetByPatientIdAndDateAsync(int patientId, DateOnly date, CancellationToken cancellationToken = default);
    Task<IEnumerable<MentalWellbeingMetric>> GetByPatientIdAsync(int patientId, CancellationToken cancellationToken = default);
    Task<IEnumerable<MentalWellbeingMetric>> GetByDateRangeAsync(int patientId, DateOnly startDate, DateOnly endDate, CancellationToken cancellationToken = default);
}



