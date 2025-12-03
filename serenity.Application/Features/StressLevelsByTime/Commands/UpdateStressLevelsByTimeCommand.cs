using MediatR;
using serenity.Application.DTOs;
using serenity.Application.UseCases.StressLevelsByTime.Commands;

namespace serenity.Application.Features.StressLevelsByTime.Commands;

public record UpdateStressLevelsByTimeCommand(int Id, UpdateStressLevelsByTimeRequest Request) : IRequest<StressLevelsByTimeDto>;

public class UpdateStressLevelsByTimeCommandHandler : IRequestHandler<UpdateStressLevelsByTimeCommand, StressLevelsByTimeDto>
{
    private readonly UpdateStressLevelsByTimeUseCase _useCase;

    public UpdateStressLevelsByTimeCommandHandler(UpdateStressLevelsByTimeUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task<StressLevelsByTimeDto> Handle(UpdateStressLevelsByTimeCommand request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(request.Id, request.Request, cancellationToken);
    }
}



