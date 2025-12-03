using serenity.Application.DTOs;
using serenity.Application.Interfaces;
using serenity.Application.UseCases.MentalWellbeingMetrics;
using serenity.Infrastructure;

namespace serenity.Application.UseCases.MentalWellbeingMetrics.Commands;

public class CreateMentalWellbeingMetricUseCase
{
    private readonly IMentalWellbeingMetricRepository _metricRepository;
    private readonly IPatientRepository _patientRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateMentalWellbeingMetricUseCase(
        IMentalWellbeingMetricRepository metricRepository,
        IPatientRepository patientRepository,
        IUnitOfWork unitOfWork)
    {
        _metricRepository = metricRepository;
        _patientRepository = patientRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<MentalWellbeingMetricDto> ExecuteAsync(CreateMentalWellbeingMetricRequest request, CancellationToken cancellationToken = default)
    {
        var patient = await _patientRepository.GetByIdAsync(request.PatientId, cancellationToken);
        if (patient is null)
        {
            throw new KeyNotFoundException($"No se encontró el paciente con id {request.PatientId}.");
        }

        var now = DateTime.Now;
        var metric = new MentalWellbeingMetric
        {
            PatientId = request.PatientId,
            Date = request.Date,
            StressLevel = request.StressLevel,
            EnergyLevel = request.EnergyLevel,
            ConcentrationLevel = request.ConcentrationLevel,
            SatisfactionLevel = request.SatisfactionLevel,
            SleepDuration = request.SleepDuration,
            SleepQuality = request.SleepQuality,
            MeditationMinutes = request.MeditationMinutes ?? 0,
            MeditationSessions = request.MeditationSessions ?? 0,
            CreatedAt = now,
            UpdatedAt = now
        };

        await _metricRepository.AddAsync(metric, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return metric.ToDto();
    }
}



