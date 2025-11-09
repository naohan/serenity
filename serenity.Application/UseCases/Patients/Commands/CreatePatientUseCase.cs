using serenity.Application.DTOs;
using serenity.Application.Interfaces;
using serenity.Infrastructure;

namespace serenity.Application.UseCases.Patients.Commands;

public class CreatePatientUseCase
{
    private readonly IPatientRepository _patientRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreatePatientUseCase(IPatientRepository patientRepository, IUnitOfWork unitOfWork)
    {
        _patientRepository = patientRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<PatientDto> ExecuteAsync(CreatePatientRequest request, CancellationToken cancellationToken = default)
    {
        if (request.UserId <= 0)
        {
            throw new ArgumentException("UserId debe ser válido.", nameof(request.UserId));
        }

        var existing = await _patientRepository.GetByUserIdAsync(request.UserId, cancellationToken);
        if (existing is not null)
        {
            throw new InvalidOperationException("El usuario ya cuenta con un registro de paciente.");
        }

        var patient = new Patient
        {
            UserId = request.UserId,
            PsychologistId = request.PsychologistId,
            Name = request.Name,
            Age = request.Age,
            Diagnosis = request.Diagnosis,
            AvatarUrl = request.AvatarUrl,
            Status = request.Status,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _patientRepository.AddAsync(patient, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return patient.ToDto();
    }
}

