using MediatR;
using serenity.Application.UseCases.Psychologists.Commands;

namespace serenity.Application.Features.Psychologists.Commands;

public record DeletePsychologistCommand(int Id) : IRequest<Unit>;

public class DeletePsychologistCommandHandler : IRequestHandler<DeletePsychologistCommand, Unit>
{
    private readonly DeletePsychologistUseCase _useCase;

    public DeletePsychologistCommandHandler(DeletePsychologistUseCase useCase)
    {
        _useCase = useCase;
    }

    public async Task<Unit> Handle(DeletePsychologistCommand request, CancellationToken cancellationToken)
    {
        await _useCase.ExecuteAsync(request.Id, cancellationToken);
        return Unit.Value;
    }
}

