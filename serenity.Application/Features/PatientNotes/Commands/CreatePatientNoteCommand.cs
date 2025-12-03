using MediatR;
using serenity.Application.DTOs;
using serenity.Application.UseCases.PatientNotes.Commands;

namespace serenity.Application.Features.PatientNotes.Commands;

public record CreatePatientNoteCommand(CreatePatientNoteRequest Request) : IRequest<PatientNoteDto>;

public class CreatePatientNoteCommandHandler : IRequestHandler<CreatePatientNoteCommand, PatientNoteDto>
{
    private readonly CreatePatientNoteUseCase _useCase;

    public CreatePatientNoteCommandHandler(CreatePatientNoteUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task<PatientNoteDto> Handle(CreatePatientNoteCommand request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(request.Request, cancellationToken);
    }
}



