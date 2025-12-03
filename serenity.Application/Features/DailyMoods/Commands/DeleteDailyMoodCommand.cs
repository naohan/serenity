using MediatR;
using serenity.Application.UseCases.DailyMoods.Commands;

namespace serenity.Application.Features.DailyMoods.Commands;

public record DeleteDailyMoodCommand(int Id) : IRequest;

public class DeleteDailyMoodCommandHandler : IRequestHandler<DeleteDailyMoodCommand>
{
    private readonly DeleteDailyMoodUseCase _useCase;

    public DeleteDailyMoodCommandHandler(DeleteDailyMoodUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task Handle(DeleteDailyMoodCommand request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(request.Id, cancellationToken);
    }
}



