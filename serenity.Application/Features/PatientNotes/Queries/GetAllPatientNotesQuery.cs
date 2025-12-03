using MediatR;
using serenity.Application.DTOs;
using serenity.Application.UseCases.PatientNotes.Queries;

namespace serenity.Application.Features.PatientNotes.Queries;

public record GetAllPatientNotesQuery : IRequest<IEnumerable<PatientNoteDto>>;

public class GetAllPatientNotesQueryHandler : IRequestHandler<GetAllPatientNotesQuery, IEnumerable<PatientNoteDto>>
{
    private readonly GetAllPatientNotesUseCase _useCase;

    public GetAllPatientNotesQueryHandler(GetAllPatientNotesUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task<IEnumerable<PatientNoteDto>> Handle(GetAllPatientNotesQuery request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(cancellationToken);
    }
}



