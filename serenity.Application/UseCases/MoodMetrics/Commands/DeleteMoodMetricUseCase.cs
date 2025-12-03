using serenity.Application.Interfaces;

namespace serenity.Application.UseCases.MoodMetrics.Commands;

public class DeleteMoodMetricUseCase
{
    private readonly IMoodMetricRepository _moodMetricRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteMoodMetricUseCase(IMoodMetricRepository moodMetricRepository, IUnitOfWork unitOfWork)
    {
        _moodMetricRepository = moodMetricRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        var moodMetric = await _moodMetricRepository.GetByIdAsync(id, cancellationToken)
                        ?? throw new KeyNotFoundException($"No se encontró la métrica de ánimo con id {id}.");

        await _moodMetricRepository.DeleteAsync(moodMetric);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}




