using MediatR;
using serenity.Application.UseCases.Trainers.Commands;

namespace serenity.Application.Features.Trainers.Commands;

public record DeleteTrainerCommand(int Id) : IRequest<Unit>;

public class DeleteTrainerCommandHandler : IRequestHandler<DeleteTrainerCommand, Unit>
{
    private readonly DeleteTrainerUseCase _useCase;

    public DeleteTrainerCommandHandler(DeleteTrainerUseCase useCase)
    {
        _useCase = useCase;
    }

    public async Task<Unit> Handle(DeleteTrainerCommand request, CancellationToken cancellationToken)
    {
        await _useCase.ExecuteAsync(request.Id, cancellationToken);
        return Unit.Value;
    }
}

