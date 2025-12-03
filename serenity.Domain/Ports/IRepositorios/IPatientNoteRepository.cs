using serenity.Infrastructure;

namespace serenity.Domain.Ports.IRepositorios;

/// <summary>
/// Repository port for PatientNote domain entity.
/// </summary>
public interface IPatientNoteRepository : IRepository<PatientNote>
{
    Task<IEnumerable<PatientNote>> GetByPatientIdAsync(int patientId, CancellationToken cancellationToken = default);
    Task<IEnumerable<PatientNote>> GetByPsychologistIdAsync(int psychologistId, CancellationToken cancellationToken = default);
    Task<IEnumerable<PatientNote>> GetByDateRangeAsync(int patientId, DateOnly startDate, DateOnly endDate, CancellationToken cancellationToken = default);
}



