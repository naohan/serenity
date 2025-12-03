using serenity.Application.DTOs;
using serenity.Application.Interfaces;
using serenity.Application.UseCases.MentalWellbeingMetrics;

namespace serenity.Application.UseCases.MentalWellbeingMetrics.Commands;

public class UpdateMentalWellbeingMetricUseCase
{
    private readonly IMentalWellbeingMetricRepository _metricRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateMentalWellbeingMetricUseCase(IMentalWellbeingMetricRepository metricRepository, IUnitOfWork unitOfWork)
    {
        _metricRepository = metricRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<MentalWellbeingMetricDto> ExecuteAsync(int id, UpdateMentalWellbeingMetricRequest request, CancellationToken cancellationToken = default)
    {
        var metric = await _metricRepository.GetByIdAsync(id, cancellationToken)
                    ?? throw new KeyNotFoundException($"No se encontró la métrica de bienestar mental con id {id}.");

        if (request.Date.HasValue)
        {
            metric.Date = request.Date.Value;
        }

        if (request.StressLevel.HasValue)
        {
            metric.StressLevel = request.StressLevel;
        }

        if (request.EnergyLevel.HasValue)
        {
            metric.EnergyLevel = request.EnergyLevel;
        }

        if (request.ConcentrationLevel.HasValue)
        {
            metric.ConcentrationLevel = request.ConcentrationLevel;
        }

        if (request.SatisfactionLevel.HasValue)
        {
            metric.SatisfactionLevel = request.SatisfactionLevel;
        }

        if (request.SleepDuration.HasValue)
        {
            metric.SleepDuration = request.SleepDuration;
        }

        if (request.SleepQuality is not null)
        {
            metric.SleepQuality = request.SleepQuality;
        }

        if (request.MeditationMinutes.HasValue)
        {
            metric.MeditationMinutes = request.MeditationMinutes;
        }

        if (request.MeditationSessions.HasValue)
        {
            metric.MeditationSessions = request.MeditationSessions;
        }

        metric.UpdatedAt = DateTime.Now;

        await _metricRepository.UpdateAsync(metric);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return metric.ToDto();
    }
}



