using serenity.Infrastructure;

namespace serenity.Application.Interfaces;

public interface IMeditationSessionRepository : IRepository<MeditationSession>
{
    Task<IEnumerable<MeditationSession>> GetByPatientIdAsync(int patientId, CancellationToken cancellationToken = default);
    Task<IEnumerable<MeditationSession>> GetByDateRangeAsync(int patientId, DateOnly startDate, DateOnly endDate, CancellationToken cancellationToken = default);
}




