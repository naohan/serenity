using serenity.Application.DTOs;
using serenity.Application.Interfaces;

namespace serenity.Application.UseCases.Patients.Queries;

public class GetAllPatientsUseCase
{
    private readonly IPatientRepository _patientRepository;

    public GetAllPatientsUseCase(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    public async Task<IEnumerable<PatientDto>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var patients = await _patientRepository.GetAllAsync(cancellationToken);
        return patients.Select(p => p.ToDto());
    }
}

