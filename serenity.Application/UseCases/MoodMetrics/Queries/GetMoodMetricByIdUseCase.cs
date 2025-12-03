using serenity.Application.DTOs;
using serenity.Application.Interfaces;
using serenity.Application.UseCases.MoodMetrics;

namespace serenity.Application.UseCases.MoodMetrics.Queries;

public class GetMoodMetricByIdUseCase
{
    private readonly IMoodMetricRepository _moodMetricRepository;

    public GetMoodMetricByIdUseCase(IMoodMetricRepository moodMetricRepository)
    {
        _moodMetricRepository = moodMetricRepository;
    }

    public async Task<MoodMetricDto?> ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        var moodMetric = await _moodMetricRepository.GetByIdAsync(id, cancellationToken);
        return moodMetric?.ToDto();
    }
}



