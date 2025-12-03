using serenity.Application.DTOs;
using serenity.Application.Interfaces;
using serenity.Application.UseCases.MentalWellbeingMetrics;

namespace serenity.Application.UseCases.MentalWellbeingMetrics.Queries;

public class GetMentalWellbeingMetricByIdUseCase
{
    private readonly IMentalWellbeingMetricRepository _metricRepository;

    public GetMentalWellbeingMetricByIdUseCase(IMentalWellbeingMetricRepository metricRepository)
    {
        _metricRepository = metricRepository;
    }

    public async Task<MentalWellbeingMetricDto?> ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        var metric = await _metricRepository.GetByIdAsync(id, cancellationToken);
        return metric?.ToDto();
    }
}



