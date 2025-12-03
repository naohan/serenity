using serenity.Infrastructure;

namespace serenity.Application.Interfaces;

public interface IStressLevelsByTimeRepository : IRepository<StressLevelsByTime>
{
    Task<IEnumerable<StressLevelsByTime>> GetByPatientIdAsync(int patientId, CancellationToken cancellationToken = default);
    Task<IEnumerable<StressLevelsByTime>> GetByDateAsync(int patientId, DateOnly date, CancellationToken cancellationToken = default);
    Task<IEnumerable<StressLevelsByTime>> GetByDateRangeAsync(int patientId, DateOnly startDate, DateOnly endDate, CancellationToken cancellationToken = default);
}




