using MediatR;
using serenity.Application.DTOs;
using serenity.Application.UseCases.Trainers.Commands;

namespace serenity.Application.Features.Trainers.Commands;

public record UpdateTrainerCommand(int Id, UpdateTrainerRequest Request) : IRequest<TrainerDto>;

public class UpdateTrainerCommandHandler : IRequestHandler<UpdateTrainerCommand, TrainerDto>
{
    private readonly UpdateTrainerUseCase _useCase;

    public UpdateTrainerCommandHandler(UpdateTrainerUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task<TrainerDto> Handle(UpdateTrainerCommand request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(request.Id, request.Request, cancellationToken);
    }
}

