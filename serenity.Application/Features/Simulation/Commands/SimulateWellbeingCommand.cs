using MediatR;
using serenity.Application.DTOs;
using serenity.Application.UseCases.Simulation;

namespace serenity.Application.Features.Simulation.Commands;

public record SimulateWellbeingCommand(SimulateWellbeingRequest Request) : IRequest<SimulationResponseDto>;

public class SimulateWellbeingCommandHandler : IRequestHandler<SimulateWellbeingCommand, SimulationResponseDto>
{
    private readonly SimulateWellbeingMetricsUseCase _useCase;

    public SimulateWellbeingCommandHandler(SimulateWellbeingMetricsUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task<SimulationResponseDto> Handle(SimulateWellbeingCommand request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(request.Request, cancellationToken);
    }
}


