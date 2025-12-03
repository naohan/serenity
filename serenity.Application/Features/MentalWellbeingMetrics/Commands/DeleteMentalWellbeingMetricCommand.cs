using MediatR;
using serenity.Application.UseCases.MentalWellbeingMetrics.Commands;

namespace serenity.Application.Features.MentalWellbeingMetrics.Commands;

public record DeleteMentalWellbeingMetricCommand(int Id) : IRequest;

public class DeleteMentalWellbeingMetricCommandHandler : IRequestHandler<DeleteMentalWellbeingMetricCommand>
{
    private readonly DeleteMentalWellbeingMetricUseCase _useCase;

    public DeleteMentalWellbeingMetricCommandHandler(DeleteMentalWellbeingMetricUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task Handle(DeleteMentalWellbeingMetricCommand request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(request.Id, cancellationToken);
    }
}




