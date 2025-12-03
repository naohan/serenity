using serenity.Application.DTOs;
using serenity.Application.Interfaces;
using serenity.Application.UseCases.InsightsAndRecommendations;

namespace serenity.Application.UseCases.InsightsAndRecommendations.Queries;

public class GetAllInsightsAndRecommendationsUseCase
{
    private readonly IInsightsAndRecommendationRepository _insightRepository;

    public GetAllInsightsAndRecommendationsUseCase(IInsightsAndRecommendationRepository insightRepository)
    {
        _insightRepository = insightRepository;
    }

    public async Task<IEnumerable<InsightsAndRecommendationDto>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var insights = await _insightRepository.GetAllAsync(cancellationToken);
        return insights.Select(i => i.ToDto());
    }
}



