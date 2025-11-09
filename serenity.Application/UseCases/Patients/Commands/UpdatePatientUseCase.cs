using serenity.Application.DTOs;
using serenity.Application.Interfaces;

namespace serenity.Application.UseCases.Patients.Commands;

public class UpdatePatientUseCase
{
    private readonly IPatientRepository _patientRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePatientUseCase(IPatientRepository patientRepository, IUnitOfWork unitOfWork)
    {
        _patientRepository = patientRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<PatientDto> ExecuteAsync(int id, UpdatePatientRequest request, CancellationToken cancellationToken = default)
    {
        var patient = await _patientRepository.GetByIdAsync(id, cancellationToken)
                      ?? throw new KeyNotFoundException($"No se encontró el paciente con id {id}.");

        patient.PsychologistId = request.PsychologistId;
        patient.Name = request.Name;
        patient.Age = request.Age;
        patient.Diagnosis = request.Diagnosis;
        patient.AvatarUrl = request.AvatarUrl;
        patient.Status = request.Status;
        patient.UpdatedAt = DateTime.UtcNow;

        await _patientRepository.UpdateAsync(patient);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return patient.ToDto();
    }
}

