using MediatR;
using serenity.Application.UseCases.PatientNotes.Commands;

namespace serenity.Application.Features.PatientNotes.Commands;

public record DeletePatientNoteCommand(int Id) : IRequest;

public class DeletePatientNoteCommandHandler : IRequestHandler<DeletePatientNoteCommand>
{
    private readonly DeletePatientNoteUseCase _useCase;

    public DeletePatientNoteCommandHandler(DeletePatientNoteUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task Handle(DeletePatientNoteCommand request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(request.Id, cancellationToken);
    }
}



