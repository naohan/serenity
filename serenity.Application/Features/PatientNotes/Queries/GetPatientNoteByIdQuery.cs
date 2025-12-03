using MediatR;
using serenity.Application.DTOs;
using serenity.Application.UseCases.PatientNotes.Queries;

namespace serenity.Application.Features.PatientNotes.Queries;

public record GetPatientNoteByIdQuery(int Id) : IRequest<PatientNoteDto?>;

public class GetPatientNoteByIdQueryHandler : IRequestHandler<GetPatientNoteByIdQuery, PatientNoteDto?>
{
    private readonly GetPatientNoteByIdUseCase _useCase;

    public GetPatientNoteByIdQueryHandler(GetPatientNoteByIdUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task<PatientNoteDto?> Handle(GetPatientNoteByIdQuery request, CancellationToken cancellationToken)
    {
        return _useCase.ExecuteAsync(request.Id, cancellationToken);
    }
}




