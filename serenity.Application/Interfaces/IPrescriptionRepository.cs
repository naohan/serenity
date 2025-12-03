using serenity.Infrastructure;

namespace serenity.Application.Interfaces;

public interface IPrescriptionRepository : IRepository<Prescription>
{
    Task<IEnumerable<Prescription>> GetByPatientIdAsync(int patientId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Prescription>> GetByPsychologistIdAsync(int psychologistId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Prescription>> GetActiveByPatientIdAsync(int patientId, CancellationToken cancellationToken = default);
}




