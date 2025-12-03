using serenity.Application.DTOs;
using serenity.Application.Interfaces;
using serenity.Infrastructure;

namespace serenity.Application.UseCases.Simulation;

public class SimulateWellbeingMetricsUseCase
{
    private readonly IMentalWellbeingMetricRepository _wellbeingRepository;
    private readonly IPatientRepository _patientRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly Random _random;

    private readonly string[] _sleepQualities = { "Excelente", "Buena", "Regular", "Mala" };

    public SimulateWellbeingMetricsUseCase(
        IMentalWellbeingMetricRepository wellbeingRepository,
        IPatientRepository patientRepository,
        IUnitOfWork unitOfWork)
    {
        _wellbeingRepository = wellbeingRepository;
        _patientRepository = patientRepository;
        _unitOfWork = unitOfWork;
        _random = new Random();
    }

    public async Task<SimulationResponseDto> ExecuteAsync(SimulateWellbeingRequest request, CancellationToken cancellationToken = default)
    {
        var patient = await _patientRepository.GetByIdAsync(request.PatientId, cancellationToken)
                     ?? throw new KeyNotFoundException($"No se encontró el paciente con id {request.PatientId}.");

        var now = DateTime.Now;

        // Generar métricas de bienestar mental realistas
        var wellbeingEntity = new MentalWellbeingMetric
        {
            PatientId = request.PatientId,
            Date = request.Date,
            StressLevel = (sbyte)_random.Next(1, 8), // 1-7
            EnergyLevel = (sbyte)_random.Next(3, 9), // 3-8
            ConcentrationLevel = (sbyte)_random.Next(4, 9), // 4-8
            SatisfactionLevel = (sbyte)_random.Next(3, 9), // 3-8
            SleepDuration = (decimal)(_random.Next(6, 10) + _random.NextDouble()), // 6-9 horas
            SleepQuality = _sleepQualities[_random.Next(_sleepQualities.Length)],
            MeditationMinutes = _random.Next(0, 60), // 0-59 minutos
            MeditationSessions = (int)_random.Next(0, 4), // 0-3 sesiones
            CreatedAt = now,
            UpdatedAt = now
        };

        await _wellbeingRepository.AddAsync(wellbeingEntity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new SimulationResponseDto
        {
            PatientId = request.PatientId,
            Date = request.Date,
            SimulationType = "WellbeingMetrics",
            RecordsCreated = 1,
            Message = $"Se generó una métrica de bienestar mental para el {request.Date}"
        };
    }
}

