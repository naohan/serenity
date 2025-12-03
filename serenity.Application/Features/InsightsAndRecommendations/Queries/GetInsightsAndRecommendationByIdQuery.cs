using MediatR;
using serenity.Application.DTOs;
using serenity.Application.UseCases.InsightsAndRecommendations.Queries;

namespace serenity.Application.Features.InsightsAndRecommendations.Queries;

public record GetInsightsAndRecommendationByIdQuery(int Id) : IRequest<InsightsAndRecommendationDto?>;

public class GetInsightsAndRecommendationByIdQueryHandler : IRequestHandler<GetInsightsAndRecommendationByIdQuery, InsightsAndRecommendationDto?>
{
    private readonly GetInsightsAndRecommendationByIdUseCase _useCase;

    public GetInsightsAndRecommendationByIdQueryHandler(GetInsightsAndRecommendationByIdUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task<InsightsAndRecommendationDto?> Handle(GetInsightsAndRecommendationByIdQuery request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(request.Id, cancellationToken);
    }
}



