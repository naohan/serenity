using serenity.Infrastructure;

namespace serenity.Application.Interfaces;

public interface IPatientReportRepository : IRepository<PatientReport>
{
    Task<IEnumerable<PatientReport>> GetByPatientIdAsync(int patientId, CancellationToken cancellationToken = default);
    Task<IEnumerable<PatientReport>> GetByPsychologistIdAsync(int psychologistId, CancellationToken cancellationToken = default);
}



