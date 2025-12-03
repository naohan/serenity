using serenity.Application.DTOs;
using serenity.Application.Interfaces;
using serenity.Infrastructure;

namespace serenity.Application.UseCases.Simulation;

public class SimulateStressLevelsUseCase
{
    private readonly IStressLevelsByTimeRepository _stressRepository;
    private readonly IPatientRepository _patientRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly Random _random;

    public SimulateStressLevelsUseCase(
        IStressLevelsByTimeRepository stressRepository,
        IPatientRepository patientRepository,
        IUnitOfWork unitOfWork)
    {
        _stressRepository = stressRepository;
        _patientRepository = patientRepository;
        _unitOfWork = unitOfWork;
        _random = new Random();
    }

    public async Task<SimulationResponseDto> ExecuteAsync(SimulateStressRequest request, CancellationToken cancellationToken = default)
    {
        var patient = await _patientRepository.GetByIdAsync(request.PatientId, cancellationToken)
                     ?? throw new KeyNotFoundException($"No se encontró el paciente con id {request.PatientId}.");

        var hoursToSimulate = request.HoursToSimulate ?? 24;
        var recordsCreated = 0;
        var now = DateTime.Now;

        // Simular niveles de estrés por hora
        for (int hour = 0; hour < hoursToSimulate; hour++)
        {
            var timeOfDay = new TimeOnly(hour, _random.Next(0, 60));
            
            // Patrón realista: más estrés en horas laborales (9-17), menos en la noche
            sbyte stressLevel;
            if (hour >= 9 && hour <= 17)
            {
                // Horas laborales: estrés medio-alto (4-7)
                stressLevel = (sbyte)_random.Next(4, 8);
            }
            else if (hour >= 22 || hour <= 6)
            {
                // Noche/madrugada: estrés bajo (1-3)
                stressLevel = (sbyte)_random.Next(1, 4);
            }
            else
            {
                // Mañana/tarde: estrés medio (2-5)
                stressLevel = (sbyte)_random.Next(2, 6);
            }

            var stressEntity = new Infrastructure.StressLevelsByTime
            {
                PatientId = request.PatientId,
                Date = request.Date,
                TimeOfDay = timeOfDay,
                StressLevel = stressLevel,
                CreatedAt = now
            };

            await _stressRepository.AddAsync(stressEntity, cancellationToken);
            recordsCreated++;
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new SimulationResponseDto
        {
            PatientId = request.PatientId,
            Date = request.Date,
            SimulationType = "StressLevels",
            RecordsCreated = recordsCreated,
            Message = $"Se generaron {recordsCreated} registros de niveles de estrés para el {request.Date}"
        };
    }
}

