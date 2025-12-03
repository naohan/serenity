using serenity.Application.DTOs;
using serenity.Application.Interfaces;
using serenity.Application.UseCases.Prescriptions;
using serenity.Infrastructure;

namespace serenity.Application.UseCases.Prescriptions.Commands;

public class CreatePrescriptionUseCase
{
    private readonly IPrescriptionRepository _prescriptionRepository;
    private readonly IPatientRepository _patientRepository;
    private readonly IPsychologistRepository _psychologistRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreatePrescriptionUseCase(
        IPrescriptionRepository prescriptionRepository,
        IPatientRepository patientRepository,
        IPsychologistRepository psychologistRepository,
        IUnitOfWork unitOfWork)
    {
        _prescriptionRepository = prescriptionRepository;
        _patientRepository = patientRepository;
        _psychologistRepository = psychologistRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<PrescriptionDto> ExecuteAsync(CreatePrescriptionRequest request, CancellationToken cancellationToken = default)
    {
        var patient = await _patientRepository.GetByIdAsync(request.PatientId, cancellationToken);
        if (patient is null)
        {
            throw new KeyNotFoundException($"No se encontró el paciente con id {request.PatientId}.");
        }

        var psychologist = await _psychologistRepository.GetByIdAsync(request.PsychologistId, cancellationToken);
        if (psychologist is null)
        {
            throw new KeyNotFoundException($"No se encontró el psicólogo con id {request.PsychologistId}.");
        }

        var now = DateTime.Now;
        var prescription = new Prescription
        {
            PatientId = request.PatientId,
            PsychologistId = request.PsychologistId,
            MedicationName = request.MedicationName,
            Dosage = request.Dosage,
            Frequency = request.Frequency,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            Instructions = request.Instructions,
            Status = request.Status,
            CreatedAt = now,
            UpdatedAt = now
        };

        await _prescriptionRepository.AddAsync(prescription, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return prescription.ToDto();
    }
}




