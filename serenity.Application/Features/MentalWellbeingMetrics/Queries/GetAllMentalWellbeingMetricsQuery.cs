using MediatR;
using serenity.Application.DTOs;
using serenity.Application.UseCases.MentalWellbeingMetrics.Queries;

namespace serenity.Application.Features.MentalWellbeingMetrics.Queries;

public record GetAllMentalWellbeingMetricsQuery : IRequest<IEnumerable<MentalWellbeingMetricDto>>;

public class GetAllMentalWellbeingMetricsQueryHandler : IRequestHandler<GetAllMentalWellbeingMetricsQuery, IEnumerable<MentalWellbeingMetricDto>>
{
    private readonly GetAllMentalWellbeingMetricsUseCase _useCase;

    public GetAllMentalWellbeingMetricsQueryHandler(GetAllMentalWellbeingMetricsUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task<IEnumerable<MentalWellbeingMetricDto>> Handle(GetAllMentalWellbeingMetricsQuery request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(cancellationToken);
    }
}




