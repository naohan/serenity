using serenity.Infrastructure;

namespace serenity.Domain.Ports.IRepositorios;

/// <summary>
/// Repository port for PatientReport domain entity.
/// </summary>
public interface IPatientReportRepository : IRepository<PatientReport>
{
    Task<IEnumerable<PatientReport>> GetByPatientIdAsync(int patientId, CancellationToken cancellationToken = default);
    Task<IEnumerable<PatientReport>> GetByPsychologistIdAsync(int psychologistId, CancellationToken cancellationToken = default);
}



