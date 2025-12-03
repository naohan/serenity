using MediatR;
using serenity.Application.DTOs;
using serenity.Application.UseCases.MoodMetrics.Queries;

namespace serenity.Application.Features.MoodMetrics.Queries;

public record GetAllMoodMetricsQuery : IRequest<IEnumerable<MoodMetricDto>>;

public class GetAllMoodMetricsQueryHandler : IRequestHandler<GetAllMoodMetricsQuery, IEnumerable<MoodMetricDto>>
{
    private readonly GetAllMoodMetricsUseCase _useCase;

    public GetAllMoodMetricsQueryHandler(GetAllMoodMetricsUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task<IEnumerable<MoodMetricDto>> Handle(GetAllMoodMetricsQuery request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(cancellationToken);
    }
}




