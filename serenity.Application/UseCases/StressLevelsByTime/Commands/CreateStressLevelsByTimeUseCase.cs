using serenity.Application.DTOs;
using serenity.Application.Interfaces;
using serenity.Application.UseCases.StressLevelsByTime;
using serenity.Infrastructure;

namespace serenity.Application.UseCases.StressLevelsByTime.Commands;

public class CreateStressLevelsByTimeUseCase
{
    private readonly IStressLevelsByTimeRepository _stressLevelRepository;
    private readonly IPatientRepository _patientRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateStressLevelsByTimeUseCase(
        IStressLevelsByTimeRepository stressLevelRepository,
        IPatientRepository patientRepository,
        IUnitOfWork unitOfWork)
    {
        _stressLevelRepository = stressLevelRepository;
        _patientRepository = patientRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<StressLevelsByTimeDto> ExecuteAsync(CreateStressLevelsByTimeRequest request, CancellationToken cancellationToken = default)
    {
        var patient = await _patientRepository.GetByIdAsync(request.PatientId, cancellationToken);
        if (patient is null)
        {
            throw new KeyNotFoundException($"No se encontró el paciente con id {request.PatientId}.");
        }

        var stressLevel = new Infrastructure.StressLevelsByTime
        {
            PatientId = request.PatientId,
            Date = request.Date,
            TimeOfDay = request.TimeOfDay,
            StressLevel = request.StressLevel,
            CreatedAt = DateTime.Now
        };

        await _stressLevelRepository.AddAsync(stressLevel, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return stressLevel.ToDto();
    }
}

