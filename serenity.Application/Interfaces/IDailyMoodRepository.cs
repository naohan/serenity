using serenity.Infrastructure;

namespace serenity.Application.Interfaces;

public interface IDailyMoodRepository : IRepository<DailyMood>
{
    Task<DailyMood?> GetByPatientIdAndDateAsync(int patientId, DateOnly date, CancellationToken cancellationToken = default);
    Task<IEnumerable<DailyMood>> GetByPatientIdAsync(int patientId, CancellationToken cancellationToken = default);
    Task<IEnumerable<DailyMood>> GetByDateRangeAsync(int patientId, DateOnly startDate, DateOnly endDate, CancellationToken cancellationToken = default);
}




