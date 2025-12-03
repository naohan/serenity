using MediatR;
using serenity.Application.DTOs;
using serenity.Application.UseCases.DailyMoods.Commands;

namespace serenity.Application.Features.DailyMoods.Commands;

public record UpdateDailyMoodCommand(int Id, UpdateDailyMoodRequest Request) : IRequest<DailyMoodDto>;

public class UpdateDailyMoodCommandHandler : IRequestHandler<UpdateDailyMoodCommand, DailyMoodDto>
{
    private readonly UpdateDailyMoodUseCase _useCase;

    public UpdateDailyMoodCommandHandler(UpdateDailyMoodUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task<DailyMoodDto> Handle(UpdateDailyMoodCommand request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(request.Id, request.Request, cancellationToken);
    }
}




