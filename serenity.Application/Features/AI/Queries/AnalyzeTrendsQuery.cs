using MediatR;
using serenity.Application.DTOs;
using serenity.Application.UseCases.AI;

namespace serenity.Application.Features.AI.Queries;

public record AnalyzeTrendsQuery(int PatientId, DateOnly StartDate, DateOnly EndDate) : IRequest<TrendAnalysisResponseDto>;

public class AnalyzeTrendsQueryHandler : IRequestHandler<AnalyzeTrendsQuery, TrendAnalysisResponseDto>
{
    private readonly AnalyzeWellbeingTrendsUseCase _useCase;

    public AnalyzeTrendsQueryHandler(AnalyzeWellbeingTrendsUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task<TrendAnalysisResponseDto> Handle(AnalyzeTrendsQuery request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(request.PatientId, request.StartDate, request.EndDate, cancellationToken);
    }
}


