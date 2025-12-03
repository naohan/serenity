using MediatR;
using serenity.Application.DTOs;
using serenity.Application.UseCases.PatientNotes.Commands;

namespace serenity.Application.Features.PatientNotes.Commands;

public record UpdatePatientNoteCommand(int Id, UpdatePatientNoteRequest Request) : IRequest<PatientNoteDto>;

public class UpdatePatientNoteCommandHandler : IRequestHandler<UpdatePatientNoteCommand, PatientNoteDto>
{
    private readonly UpdatePatientNoteUseCase _useCase;

    public UpdatePatientNoteCommandHandler(UpdatePatientNoteUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task<PatientNoteDto> Handle(UpdatePatientNoteCommand request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(request.Id, request.Request, cancellationToken);
    }
}




