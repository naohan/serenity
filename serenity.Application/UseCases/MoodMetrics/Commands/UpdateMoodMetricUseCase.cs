using serenity.Application.DTOs;
using serenity.Application.Interfaces;
using serenity.Application.UseCases.MoodMetrics;

namespace serenity.Application.UseCases.MoodMetrics.Commands;

public class UpdateMoodMetricUseCase
{
    private readonly IMoodMetricRepository _moodMetricRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateMoodMetricUseCase(IMoodMetricRepository moodMetricRepository, IUnitOfWork unitOfWork)
    {
        _moodMetricRepository = moodMetricRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<MoodMetricDto> ExecuteAsync(int id, UpdateMoodMetricRequest request, CancellationToken cancellationToken = default)
    {
        var moodMetric = await _moodMetricRepository.GetByIdAsync(id, cancellationToken)
                        ?? throw new KeyNotFoundException($"No se encontró la métrica de ánimo con id {id}.");

        if (request.Date.HasValue)
        {
            moodMetric.Date = request.Date.Value;
        }

        if (request.HappyPercentage.HasValue)
        {
            moodMetric.HappyPercentage = request.HappyPercentage;
        }

        if (request.CalmPercentage.HasValue)
        {
            moodMetric.CalmPercentage = request.CalmPercentage;
        }

        if (request.SadPercentage.HasValue)
        {
            moodMetric.SadPercentage = request.SadPercentage;
        }

        if (request.AnxiousPercentage.HasValue)
        {
            moodMetric.AnxiousPercentage = request.AnxiousPercentage;
        }

        moodMetric.UpdatedAt = DateTime.Now;

        await _moodMetricRepository.UpdateAsync(moodMetric);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return moodMetric.ToDto();
    }
}




