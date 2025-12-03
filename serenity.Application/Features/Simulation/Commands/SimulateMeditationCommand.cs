using MediatR;
using serenity.Application.DTOs;
using serenity.Application.UseCases.Simulation;

namespace serenity.Application.Features.Simulation.Commands;

public record SimulateMeditationCommand(SimulateMeditationRequest Request) : IRequest<SimulationResponseDto>;

public class SimulateMeditationCommandHandler : IRequestHandler<SimulateMeditationCommand, SimulationResponseDto>
{
    private readonly SimulateMeditationSessionsUseCase _useCase;

    public SimulateMeditationCommandHandler(SimulateMeditationSessionsUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task<SimulationResponseDto> Handle(SimulateMeditationCommand request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(request.Request, cancellationToken);
    }
}


