using serenity.Infrastructure;

namespace serenity.Domain.Ports.IRepositorios;

/// <summary>
/// Repository port for Prescription domain entity.
/// </summary>
public interface IPrescriptionRepository : IRepository<Prescription>
{
    Task<IEnumerable<Prescription>> GetByPatientIdAsync(int patientId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Prescription>> GetByPsychologistIdAsync(int psychologistId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Prescription>> GetActiveByPatientIdAsync(int patientId, CancellationToken cancellationToken = default);
}



