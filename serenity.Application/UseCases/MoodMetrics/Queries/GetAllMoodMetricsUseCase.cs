using serenity.Application.DTOs;
using serenity.Application.Interfaces;
using serenity.Application.UseCases.MoodMetrics;

namespace serenity.Application.UseCases.MoodMetrics.Queries;

public class GetAllMoodMetricsUseCase
{
    private readonly IMoodMetricRepository _moodMetricRepository;

    public GetAllMoodMetricsUseCase(IMoodMetricRepository moodMetricRepository)
    {
        _moodMetricRepository = moodMetricRepository;
    }

    public async Task<IEnumerable<MoodMetricDto>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var moodMetrics = await _moodMetricRepository.GetAllAsync(cancellationToken);
        return moodMetrics.Select(m => m.ToDto());
    }
}



