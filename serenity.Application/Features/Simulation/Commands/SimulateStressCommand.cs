using MediatR;
using serenity.Application.DTOs;
using serenity.Application.UseCases.Simulation;

namespace serenity.Application.Features.Simulation.Commands;

public record SimulateStressCommand(SimulateStressRequest Request) : IRequest<SimulationResponseDto>;

public class SimulateStressCommandHandler : IRequestHandler<SimulateStressCommand, SimulationResponseDto>
{
    private readonly SimulateStressLevelsUseCase _useCase;

    public SimulateStressCommandHandler(SimulateStressLevelsUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task<SimulationResponseDto> Handle(SimulateStressCommand request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(request.Request, cancellationToken);
    }
}


