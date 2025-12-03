using serenity.Application.DTOs;
using serenity.Application.Interfaces;
using serenity.Infrastructure;

namespace serenity.Application.UseCases.Simulation;

public class SimulateMeditationSessionsUseCase
{
    private readonly IMeditationSessionRepository _meditationRepository;
    private readonly IPatientRepository _patientRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly Random _random;

    private readonly string[] _meditationTypes = 
    { 
        "Mindfulness", 
        "Respiración", 
        "Guiada", 
        "Body Scan", 
        "Loving-Kindness",
        "Transcendental"
    };

    public SimulateMeditationSessionsUseCase(
        IMeditationSessionRepository meditationRepository,
        IPatientRepository patientRepository,
        IUnitOfWork unitOfWork)
    {
        _meditationRepository = meditationRepository;
        _patientRepository = patientRepository;
        _unitOfWork = unitOfWork;
        _random = new Random();
    }

    public async Task<SimulationResponseDto> ExecuteAsync(SimulateMeditationRequest request, CancellationToken cancellationToken = default)
    {
        var patient = await _patientRepository.GetByIdAsync(request.PatientId, cancellationToken)
                     ?? throw new KeyNotFoundException($"No se encontró el paciente con id {request.PatientId}.");

        var numberOfSessions = request.NumberOfSessions ?? _random.Next(1, 4);
        var recordsCreated = 0;
        var now = DateTime.Now;

        // Generar sesiones de meditación distribuidas a lo largo del día
        var sessionTimes = new List<TimeOnly>();
        for (int i = 0; i < numberOfSessions; i++)
        {
            var hour = _random.Next(6, 22); // Entre 6 AM y 10 PM
            var minute = _random.Next(0, 60);
            sessionTimes.Add(new TimeOnly(hour, minute));
        }

        sessionTimes = sessionTimes.OrderBy(t => t).ToList();

        foreach (var sessionTime in sessionTimes)
        {
            var duration = _random.Next(5, 31); // 5-30 minutos
            var type = _meditationTypes[_random.Next(_meditationTypes.Length)];

            var meditationEntity = new MeditationSession
            {
                PatientId = request.PatientId,
                SessionDate = request.Date,
                DurationMinutes = duration,
                Type = type,
                Notes = $"Sesión de meditación {type} realizada a las {sessionTime:HH:mm}",
                CreatedAt = now
            };

            await _meditationRepository.AddAsync(meditationEntity, cancellationToken);
            recordsCreated++;
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new SimulationResponseDto
        {
            PatientId = request.PatientId,
            Date = request.Date,
            SimulationType = "MeditationSessions",
            RecordsCreated = recordsCreated,
            Message = $"Se generaron {recordsCreated} sesiones de meditación para el {request.Date}"
        };
    }
}


