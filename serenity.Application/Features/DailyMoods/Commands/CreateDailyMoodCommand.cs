using MediatR;
using serenity.Application.DTOs;
using serenity.Application.UseCases.DailyMoods.Commands;

namespace serenity.Application.Features.DailyMoods.Commands;

public record CreateDailyMoodCommand(CreateDailyMoodRequest Request) : IRequest<DailyMoodDto>;

public class CreateDailyMoodCommandHandler : IRequestHandler<CreateDailyMoodCommand, DailyMoodDto>
{
    private readonly CreateDailyMoodUseCase _useCase;

    public CreateDailyMoodCommandHandler(CreateDailyMoodUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task<DailyMoodDto> Handle(CreateDailyMoodCommand request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(request.Request, cancellationToken);
    }
}



