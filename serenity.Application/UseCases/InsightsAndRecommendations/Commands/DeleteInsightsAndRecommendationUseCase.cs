using serenity.Application.Interfaces;

namespace serenity.Application.UseCases.InsightsAndRecommendations.Commands;

public class DeleteInsightsAndRecommendationUseCase
{
    private readonly IInsightsAndRecommendationRepository _insightRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteInsightsAndRecommendationUseCase(IInsightsAndRecommendationRepository insightRepository, IUnitOfWork unitOfWork)
    {
        _insightRepository = insightRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        var insight = await _insightRepository.GetByIdAsync(id, cancellationToken)
                     ?? throw new KeyNotFoundException($"No se encontró el insight con id {id}.");

        await _insightRepository.DeleteAsync(insight);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}



