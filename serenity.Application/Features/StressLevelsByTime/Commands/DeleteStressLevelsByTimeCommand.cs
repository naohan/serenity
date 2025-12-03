using MediatR;
using serenity.Application.UseCases.StressLevelsByTime.Commands;

namespace serenity.Application.Features.StressLevelsByTime.Commands;

public record DeleteStressLevelsByTimeCommand(int Id) : IRequest;

public class DeleteStressLevelsByTimeCommandHandler : IRequestHandler<DeleteStressLevelsByTimeCommand>
{
    private readonly DeleteStressLevelsByTimeUseCase _useCase;

    public DeleteStressLevelsByTimeCommandHandler(DeleteStressLevelsByTimeUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task Handle(DeleteStressLevelsByTimeCommand request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(request.Id, cancellationToken);
    }
}



