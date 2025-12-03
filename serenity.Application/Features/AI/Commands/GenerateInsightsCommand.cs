using MediatR;
using serenity.Application.DTOs;
using serenity.Application.UseCases.AI;

namespace serenity.Application.Features.AI.Commands;

public record GenerateInsightsCommand(int PatientId) : IRequest<InsightResponseDto>;

public class GenerateInsightsCommandHandler : IRequestHandler<GenerateInsightsCommand, InsightResponseDto>
{
    private readonly GenerateAIRecommendationsForPatientUseCase _useCase;

    public GenerateInsightsCommandHandler(GenerateAIRecommendationsForPatientUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task<InsightResponseDto> Handle(GenerateInsightsCommand request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(request.PatientId, cancellationToken);
    }
}


