using MediatR;
using serenity.Application.DTOs;
using serenity.Application.UseCases.Simulation;

namespace serenity.Application.Features.Simulation.Commands;

public record SimulateFullDayCommand(SimulateFullDayRequest Request) : IRequest<SimulationResponseDto>;

public class SimulateFullDayCommandHandler : IRequestHandler<SimulateFullDayCommand, SimulationResponseDto>
{
    private readonly SimulateFullDayMetricsUseCase _useCase;

    public SimulateFullDayCommandHandler(SimulateFullDayMetricsUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task<SimulationResponseDto> Handle(SimulateFullDayCommand request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(request.Request, cancellationToken);
    }
}


