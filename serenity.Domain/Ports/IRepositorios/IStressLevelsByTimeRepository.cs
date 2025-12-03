using serenity.Infrastructure;

namespace serenity.Domain.Ports.IRepositorios;

/// <summary>
/// Repository port for StressLevelsByTime domain entity.
/// </summary>
public interface IStressLevelsByTimeRepository : IRepository<StressLevelsByTime>
{
    Task<IEnumerable<StressLevelsByTime>> GetByPatientIdAsync(int patientId, CancellationToken cancellationToken = default);
    Task<IEnumerable<StressLevelsByTime>> GetByDateAsync(int patientId, DateOnly date, CancellationToken cancellationToken = default);
    Task<IEnumerable<StressLevelsByTime>> GetByDateRangeAsync(int patientId, DateOnly startDate, DateOnly endDate, CancellationToken cancellationToken = default);
}




