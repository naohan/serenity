using MediatR;
using serenity.Application.UseCases.MoodMetrics.Commands;

namespace serenity.Application.Features.MoodMetrics.Commands;

public record DeleteMoodMetricCommand(int Id) : IRequest;

public class DeleteMoodMetricCommandHandler : IRequestHandler<DeleteMoodMetricCommand>
{
    private readonly DeleteMoodMetricUseCase _useCase;

    public DeleteMoodMetricCommandHandler(DeleteMoodMetricUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task Handle(DeleteMoodMetricCommand request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(request.Id, cancellationToken);
    }
}



