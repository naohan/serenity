using MediatR;
using serenity.Application.UseCases.InsightsAndRecommendations.Commands;

namespace serenity.Application.Features.InsightsAndRecommendations.Commands;

public record DeleteInsightsAndRecommendationCommand(int Id) : IRequest;

public class DeleteInsightsAndRecommendationCommandHandler : IRequestHandler<DeleteInsightsAndRecommendationCommand>
{
    private readonly DeleteInsightsAndRecommendationUseCase _useCase;

    public DeleteInsightsAndRecommendationCommandHandler(DeleteInsightsAndRecommendationUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task Handle(DeleteInsightsAndRecommendationCommand request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(request.Id, cancellationToken);
    }
}




