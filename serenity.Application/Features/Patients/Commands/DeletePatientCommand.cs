using MediatR;
using serenity.Application.UseCases.Patients.Commands;

namespace serenity.Application.Features.Patients.Commands;

public record DeletePatientCommand(int Id) : IRequest<Unit>;

public class DeletePatientCommandHandler : IRequestHandler<DeletePatientCommand, Unit>
{
    private readonly DeletePatientUseCase _useCase;

    public DeletePatientCommandHandler(DeletePatientUseCase useCase)
    {
        _useCase = useCase;
    }

    public async Task<Unit> Handle(DeletePatientCommand request, CancellationToken cancellationToken)
    {
        await _useCase.ExecuteAsync(request.Id, cancellationToken);
        return Unit.Value;
    }
}

