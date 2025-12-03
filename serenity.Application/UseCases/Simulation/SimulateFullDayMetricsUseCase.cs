using serenity.Application.DTOs;
using serenity.Application.Interfaces;
using serenity.Infrastructure;

namespace serenity.Application.UseCases.Simulation;

public class SimulateFullDayMetricsUseCase
{
    private readonly IDailyMoodRepository _dailyMoodRepository;
    private readonly IEmotionalStateRepository _emotionalStateRepository;
    private readonly IMoodMetricRepository _moodMetricRepository;
    private readonly IMentalWellbeingMetricRepository _wellbeingRepository;
    private readonly IMeditationSessionRepository _meditationRepository;
    private readonly IStressLevelsByTimeRepository _stressRepository;
    private readonly IPatientRepository _patientRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly Random _random;

    private readonly string[] _moodDescriptions = 
    { 
        "Tranquilo", "Feliz", "Ansioso", "Relajado", "Eufórico", 
        "Melancólico", "Energético", "Cansado", "Motivado", "Sereno" 
    };

    private readonly string[] _emotionalStates = 
    { 
        "Alegría", "Tristeza", "Ansiedad", "Calma", "Enojo", 
        "Miedo", "Esperanza", "Gratitud", "Frustración", "Paz" 
    };

    public SimulateFullDayMetricsUseCase(
        IDailyMoodRepository dailyMoodRepository,
        IEmotionalStateRepository emotionalStateRepository,
        IMoodMetricRepository moodMetricRepository,
        IMentalWellbeingMetricRepository wellbeingRepository,
        IMeditationSessionRepository meditationRepository,
        IStressLevelsByTimeRepository stressRepository,
        IPatientRepository patientRepository,
        IUnitOfWork unitOfWork)
    {
        _dailyMoodRepository = dailyMoodRepository;
        _emotionalStateRepository = emotionalStateRepository;
        _moodMetricRepository = moodMetricRepository;
        _wellbeingRepository = wellbeingRepository;
        _meditationRepository = meditationRepository;
        _stressRepository = stressRepository;
        _patientRepository = patientRepository;
        _unitOfWork = unitOfWork;
        _random = new Random();
    }

    public async Task<SimulationResponseDto> ExecuteAsync(SimulateFullDayRequest request, CancellationToken cancellationToken = default)
    {
        var patient = await _patientRepository.GetByIdAsync(request.PatientId, cancellationToken)
                     ?? throw new KeyNotFoundException($"No se encontró el paciente con id {request.PatientId}.");

        var now = DateTime.Now;
        var totalRecords = 0;

        // 1. Daily Mood
        var dailyMood = new DailyMood
        {
            PatientId = request.PatientId,
            Date = request.Date,
            Mood = (sbyte)_random.Next(1, 8), // 1-7
            Note = _moodDescriptions[_random.Next(_moodDescriptions.Length)],
            CreatedAt = now
        };
        await _dailyMoodRepository.AddAsync(dailyMood, cancellationToken);
        totalRecords++;

        // 2. Emotional States (2-4 estados durante el día)
        var emotionalStatesCount = _random.Next(2, 5);
        for (int i = 0; i < emotionalStatesCount; i++)
        {
            var emotionalState = new EmotionalState
            {
                PatientId = request.PatientId,
                Date = request.Date,
                EmotionalState1 = _emotionalStates[_random.Next(_emotionalStates.Length)],
                Value = (decimal)(_random.Next(1, 11) + _random.NextDouble()), // 1.0-10.9
                CreatedAt = now
            };
            await _emotionalStateRepository.AddAsync(emotionalState, cancellationToken);
            totalRecords++;
        }

        // 3. Mood Metrics
        var moodMetric = new MoodMetric
        {
            PatientId = request.PatientId,
            Date = request.Date,
            HappyPercentage = (decimal)(_random.Next(20, 80) + _random.NextDouble()),
            CalmPercentage = (decimal)(_random.Next(30, 70) + _random.NextDouble()),
            SadPercentage = (decimal)(_random.Next(0, 30) + _random.NextDouble()),
            AnxiousPercentage = (decimal)(_random.Next(0, 40) + _random.NextDouble()),
            CreatedAt = now,
            UpdatedAt = now
        };
        await _moodMetricRepository.AddAsync(moodMetric, cancellationToken);
        totalRecords++;

        // 4. Mental Wellbeing Metrics
        var wellbeingMetric = new MentalWellbeingMetric
        {
            PatientId = request.PatientId,
            Date = request.Date,
            StressLevel = (sbyte)_random.Next(1, 8),
            EnergyLevel = (sbyte)_random.Next(3, 9),
            ConcentrationLevel = (sbyte)_random.Next(4, 9),
            SatisfactionLevel = (sbyte)_random.Next(3, 9),
            SleepDuration = (decimal)(_random.Next(6, 10) + _random.NextDouble()),
            SleepQuality = new[] { "Excelente", "Buena", "Regular", "Mala" }[_random.Next(4)],
            MeditationMinutes = _random.Next(0, 60),
            MeditationSessions = _random.Next(0, 4),
            CreatedAt = now,
            UpdatedAt = now
        };
        await _wellbeingRepository.AddAsync(wellbeingMetric, cancellationToken);
        totalRecords++;

        // 5. Meditation Sessions (1-3 sesiones)
        var meditationCount = _random.Next(1, 4);
        var meditationTypes = new[] { "Mindfulness", "Respiración", "Guiada", "Body Scan", "Loving-Kindness" };
        for (int i = 0; i < meditationCount; i++)
        {
            var hour = _random.Next(6, 22);
            var minute = _random.Next(0, 60);
            var meditation = new MeditationSession
            {
                PatientId = request.PatientId,
                SessionDate = request.Date,
                DurationMinutes = _random.Next(5, 31),
                Type = meditationTypes[_random.Next(meditationTypes.Length)],
                Notes = $"Sesión de meditación realizada a las {hour:D2}:{minute:D2}",
                CreatedAt = now
            };
            await _meditationRepository.AddAsync(meditation, cancellationToken);
            totalRecords++;
        }

        // 6. Stress Levels by Time (cada 2 horas)
        for (int hour = 0; hour < 24; hour += 2)
        {
            sbyte stressLevel;
            if (hour >= 9 && hour <= 17)
            {
                stressLevel = (sbyte)_random.Next(4, 8);
            }
            else if (hour >= 22 || hour <= 6)
            {
                stressLevel = (sbyte)_random.Next(1, 4);
            }
            else
            {
                stressLevel = (sbyte)_random.Next(2, 6);
            }

            var stress = new Infrastructure.StressLevelsByTime
            {
                PatientId = request.PatientId,
                Date = request.Date,
                TimeOfDay = new TimeOnly(hour, _random.Next(0, 60)),
                StressLevel = stressLevel,
                CreatedAt = now
            };
            await _stressRepository.AddAsync(stress, cancellationToken);
            totalRecords++;
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new SimulationResponseDto
        {
            PatientId = request.PatientId,
            Date = request.Date,
            SimulationType = "FullDay",
            RecordsCreated = totalRecords,
            Message = $"Se generaron {totalRecords} registros completos para el día {request.Date}"
        };
    }
}


