using serenity.Application.DTOs;
using serenity.Application.Interfaces;

namespace serenity.Application.UseCases.Patients.Queries;

public class GetPatientByIdUseCase
{
    private readonly IPatientRepository _patientRepository;

    public GetPatientByIdUseCase(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    public async Task<PatientDto?> ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        var patient = await _patientRepository.GetByIdAsync(id, cancellationToken);
        return patient?.ToDto();
    }
}

