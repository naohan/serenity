using serenity.Application.DTOs;
using serenity.Application.Interfaces;
using serenity.Application.UseCases.DailyMoods;
using serenity.Infrastructure;

namespace serenity.Application.UseCases.DailyMoods.Commands;

public class CreateDailyMoodUseCase
{
    private readonly IDailyMoodRepository _dailyMoodRepository;
    private readonly IPatientRepository _patientRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateDailyMoodUseCase(
        IDailyMoodRepository dailyMoodRepository,
        IPatientRepository patientRepository,
        IUnitOfWork unitOfWork)
    {
        _dailyMoodRepository = dailyMoodRepository;
        _patientRepository = patientRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<DailyMoodDto> ExecuteAsync(CreateDailyMoodRequest request, CancellationToken cancellationToken = default)
    {
        var patient = await _patientRepository.GetByIdAsync(request.PatientId, cancellationToken);
        if (patient is null)
        {
            throw new KeyNotFoundException($"No se encontró el paciente con id {request.PatientId}.");
        }

        var existing = await _dailyMoodRepository.GetByPatientIdAndDateAsync(request.PatientId, request.Date, cancellationToken);
        if (existing is not null)
        {
            throw new InvalidOperationException($"Ya existe un registro de estado de ánimo para el paciente {request.PatientId} en la fecha {request.Date}.");
        }

        var dailyMood = new DailyMood
        {
            PatientId = request.PatientId,
            Date = request.Date,
            Mood = request.Mood,
            Note = request.Note,
            CreatedAt = DateTime.Now
        };

        await _dailyMoodRepository.AddAsync(dailyMood, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return dailyMood.ToDto();
    }
}



