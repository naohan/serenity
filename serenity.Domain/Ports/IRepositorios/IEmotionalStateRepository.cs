using serenity.Infrastructure;

namespace serenity.Domain.Ports.IRepositorios;

/// <summary>
/// Repository port for EmotionalState domain entity.
/// </summary>
public interface IEmotionalStateRepository : IRepository<EmotionalState>
{
    Task<IEnumerable<EmotionalState>> GetByPatientIdAsync(int patientId, CancellationToken cancellationToken = default);
    Task<IEnumerable<EmotionalState>> GetByDateRangeAsync(int patientId, DateOnly startDate, DateOnly endDate, CancellationToken cancellationToken = default);
}




