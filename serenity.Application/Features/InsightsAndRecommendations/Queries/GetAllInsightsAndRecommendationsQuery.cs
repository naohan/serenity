using MediatR;
using serenity.Application.DTOs;
using serenity.Application.UseCases.InsightsAndRecommendations.Queries;

namespace serenity.Application.Features.InsightsAndRecommendations.Queries;

public record GetAllInsightsAndRecommendationsQuery : IRequest<IEnumerable<InsightsAndRecommendationDto>>;

public class GetAllInsightsAndRecommendationsQueryHandler : IRequestHandler<GetAllInsightsAndRecommendationsQuery, IEnumerable<InsightsAndRecommendationDto>>
{
    private readonly GetAllInsightsAndRecommendationsUseCase _useCase;

    public GetAllInsightsAndRecommendationsQueryHandler(GetAllInsightsAndRecommendationsUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task<IEnumerable<InsightsAndRecommendationDto>> Handle(GetAllInsightsAndRecommendationsQuery request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(cancellationToken);
    }
}



