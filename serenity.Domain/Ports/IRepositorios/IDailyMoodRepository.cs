using serenity.Infrastructure;

namespace serenity.Domain.Ports.IRepositorios;

/// <summary>
/// Repository port for DailyMood domain entity.
/// </summary>
public interface IDailyMoodRepository : IRepository<DailyMood>
{
    Task<DailyMood?> GetByPatientIdAndDateAsync(int patientId, DateOnly date, CancellationToken cancellationToken = default);
    Task<IEnumerable<DailyMood>> GetByPatientIdAsync(int patientId, CancellationToken cancellationToken = default);
    Task<IEnumerable<DailyMood>> GetByDateRangeAsync(int patientId, DateOnly startDate, DateOnly endDate, CancellationToken cancellationToken = default);
}



