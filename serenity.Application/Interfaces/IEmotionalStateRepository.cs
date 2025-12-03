using serenity.Infrastructure;

namespace serenity.Application.Interfaces;

public interface IEmotionalStateRepository : IRepository<EmotionalState>
{
    Task<IEnumerable<EmotionalState>> GetByPatientIdAsync(int patientId, CancellationToken cancellationToken = default);
    Task<IEnumerable<EmotionalState>> GetByDateRangeAsync(int patientId, DateOnly startDate, DateOnly endDate, CancellationToken cancellationToken = default);
}




