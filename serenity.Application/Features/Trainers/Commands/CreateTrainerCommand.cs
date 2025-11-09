using MediatR;
using serenity.Application.DTOs;
using serenity.Application.UseCases.Trainers.Commands;

namespace serenity.Application.Features.Trainers.Commands;

public record CreateTrainerCommand(CreateTrainerRequest Request) : IRequest<TrainerDto>;

public class CreateTrainerCommandHandler : IRequestHandler<CreateTrainerCommand, TrainerDto>
{
    private readonly CreateTrainerUseCase _useCase;

    public CreateTrainerCommandHandler(CreateTrainerUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task<TrainerDto> Handle(CreateTrainerCommand request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(request.Request, cancellationToken);
    }
}

