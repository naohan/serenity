using MediatR;
using serenity.Application.DTOs;
using serenity.Application.UseCases.StressLevelsByTime.Commands;

namespace serenity.Application.Features.StressLevelsByTime.Commands;

public record CreateStressLevelsByTimeCommand(CreateStressLevelsByTimeRequest Request) : IRequest<StressLevelsByTimeDto>;

public class CreateStressLevelsByTimeCommandHandler : IRequestHandler<CreateStressLevelsByTimeCommand, StressLevelsByTimeDto>
{
    private readonly CreateStressLevelsByTimeUseCase _useCase;

    public CreateStressLevelsByTimeCommandHandler(CreateStressLevelsByTimeUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task<StressLevelsByTimeDto> Handle(CreateStressLevelsByTimeCommand request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(request.Request, cancellationToken);
    }
}



