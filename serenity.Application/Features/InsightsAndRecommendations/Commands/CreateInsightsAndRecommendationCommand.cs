using MediatR;
using serenity.Application.DTOs;
using serenity.Application.UseCases.InsightsAndRecommendations.Commands;

namespace serenity.Application.Features.InsightsAndRecommendations.Commands;

public record CreateInsightsAndRecommendationCommand(CreateInsightsAndRecommendationRequest Request) : IRequest<InsightsAndRecommendationDto>;

public class CreateInsightsAndRecommendationCommandHandler : IRequestHandler<CreateInsightsAndRecommendationCommand, InsightsAndRecommendationDto>
{
    private readonly CreateInsightsAndRecommendationUseCase _useCase;

    public CreateInsightsAndRecommendationCommandHandler(CreateInsightsAndRecommendationUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task<InsightsAndRecommendationDto> Handle(CreateInsightsAndRecommendationCommand request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(request.Request, cancellationToken);
    }
}




