using serenity.Application.DTOs;
using serenity.Application.Interfaces;
using serenity.Application.UseCases.InsightsAndRecommendations;

namespace serenity.Application.UseCases.InsightsAndRecommendations.Queries;

public class GetInsightsAndRecommendationByIdUseCase
{
    private readonly IInsightsAndRecommendationRepository _insightRepository;

    public GetInsightsAndRecommendationByIdUseCase(IInsightsAndRecommendationRepository insightRepository)
    {
        _insightRepository = insightRepository;
    }

    public async Task<InsightsAndRecommendationDto?> ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        var insight = await _insightRepository.GetByIdAsync(id, cancellationToken);
        return insight?.ToDto();
    }
}



