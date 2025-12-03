using MediatR;
using serenity.Application.DTOs;
using serenity.Application.UseCases.MentalWellbeingMetrics.Commands;

namespace serenity.Application.Features.MentalWellbeingMetrics.Commands;

public record CreateMentalWellbeingMetricCommand(CreateMentalWellbeingMetricRequest Request) : IRequest<MentalWellbeingMetricDto>;

public class CreateMentalWellbeingMetricCommandHandler : IRequestHandler<CreateMentalWellbeingMetricCommand, MentalWellbeingMetricDto>
{
    private readonly CreateMentalWellbeingMetricUseCase _useCase;

    public CreateMentalWellbeingMetricCommandHandler(CreateMentalWellbeingMetricUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task<MentalWellbeingMetricDto> Handle(CreateMentalWellbeingMetricCommand request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(request.Request, cancellationToken);
    }
}



