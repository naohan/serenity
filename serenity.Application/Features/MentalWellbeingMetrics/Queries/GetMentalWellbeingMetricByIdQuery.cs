using MediatR;
using serenity.Application.DTOs;
using serenity.Application.UseCases.MentalWellbeingMetrics.Queries;

namespace serenity.Application.Features.MentalWellbeingMetrics.Queries;

public record GetMentalWellbeingMetricByIdQuery(int Id) : IRequest<MentalWellbeingMetricDto?>;

public class GetMentalWellbeingMetricByIdQueryHandler : IRequestHandler<GetMentalWellbeingMetricByIdQuery, MentalWellbeingMetricDto?>
{
    private readonly GetMentalWellbeingMetricByIdUseCase _useCase;

    public GetMentalWellbeingMetricByIdQueryHandler(GetMentalWellbeingMetricByIdUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task<MentalWellbeingMetricDto?> Handle(GetMentalWellbeingMetricByIdQuery request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(request.Id, cancellationToken);
    }
}



