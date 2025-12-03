using serenity.Application.Interfaces;

namespace serenity.Application.UseCases.MentalWellbeingMetrics.Commands;

public class DeleteMentalWellbeingMetricUseCase
{
    private readonly IMentalWellbeingMetricRepository _metricRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteMentalWellbeingMetricUseCase(IMentalWellbeingMetricRepository metricRepository, IUnitOfWork unitOfWork)
    {
        _metricRepository = metricRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        var metric = await _metricRepository.GetByIdAsync(id, cancellationToken)
                    ?? throw new KeyNotFoundException($"No se encontró la métrica de bienestar mental con id {id}.");

        await _metricRepository.DeleteAsync(metric);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}




