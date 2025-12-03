using MediatR;
using serenity.Application.DTOs;
using serenity.Application.UseCases.MoodMetrics.Queries;

namespace serenity.Application.Features.MoodMetrics.Queries;

public record GetMoodMetricByIdQuery(int Id) : IRequest<MoodMetricDto?>;

public class GetMoodMetricByIdQueryHandler : IRequestHandler<GetMoodMetricByIdQuery, MoodMetricDto?>
{
    private readonly GetMoodMetricByIdUseCase _useCase;

    public GetMoodMetricByIdQueryHandler(GetMoodMetricByIdUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task<MoodMetricDto?> Handle(GetMoodMetricByIdQuery request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(request.Id, cancellationToken);
    }
}



