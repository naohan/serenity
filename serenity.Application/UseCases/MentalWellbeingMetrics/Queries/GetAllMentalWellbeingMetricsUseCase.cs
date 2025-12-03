using serenity.Application.DTOs;
using serenity.Application.Interfaces;
using serenity.Application.UseCases.MentalWellbeingMetrics;

namespace serenity.Application.UseCases.MentalWellbeingMetrics.Queries;

public class GetAllMentalWellbeingMetricsUseCase
{
    private readonly IMentalWellbeingMetricRepository _metricRepository;

    public GetAllMentalWellbeingMetricsUseCase(IMentalWellbeingMetricRepository metricRepository)
    {
        _metricRepository = metricRepository;
    }

    public async Task<IEnumerable<MentalWellbeingMetricDto>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var metrics = await _metricRepository.GetAllAsync(cancellationToken);
        return metrics.Select(m => m.ToDto());
    }
}




