using MediatR;
using serenity.Application.DTOs;
using serenity.Application.UseCases.MentalWellbeingMetrics.Commands;

namespace serenity.Application.Features.MentalWellbeingMetrics.Commands;

public record UpdateMentalWellbeingMetricCommand(int Id, UpdateMentalWellbeingMetricRequest Request) : IRequest<MentalWellbeingMetricDto>;

public class UpdateMentalWellbeingMetricCommandHandler : IRequestHandler<UpdateMentalWellbeingMetricCommand, MentalWellbeingMetricDto>
{
    private readonly UpdateMentalWellbeingMetricUseCase _useCase;

    public UpdateMentalWellbeingMetricCommandHandler(UpdateMentalWellbeingMetricUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task<MentalWellbeingMetricDto> Handle(UpdateMentalWellbeingMetricCommand request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(request.Id, request.Request, cancellationToken);
    }
}



