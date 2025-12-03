using MediatR;
using serenity.Application.DTOs;
using serenity.Application.UseCases.InsightsAndRecommendations.Commands;

namespace serenity.Application.Features.InsightsAndRecommendations.Commands;

public record UpdateInsightsAndRecommendationCommand(int Id, UpdateInsightsAndRecommendationRequest Request) : IRequest<InsightsAndRecommendationDto>;

public class UpdateInsightsAndRecommendationCommandHandler : IRequestHandler<UpdateInsightsAndRecommendationCommand, InsightsAndRecommendationDto>
{
    private readonly UpdateInsightsAndRecommendationUseCase _useCase;

    public UpdateInsightsAndRecommendationCommandHandler(UpdateInsightsAndRecommendationUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task<InsightsAndRecommendationDto> Handle(UpdateInsightsAndRecommendationCommand request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(request.Id, request.Request, cancellationToken);
    }
}




