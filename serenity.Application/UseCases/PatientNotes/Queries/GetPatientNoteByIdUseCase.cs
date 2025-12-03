using serenity.Application.DTOs;
using serenity.Application.Interfaces;
using serenity.Application.UseCases.PatientNotes;

namespace serenity.Application.UseCases.PatientNotes.Queries;

public class GetPatientNoteByIdUseCase
{
    private readonly IPatientNoteRepository _noteRepository;

    public GetPatientNoteByIdUseCase(IPatientNoteRepository noteRepository)
    {
        _noteRepository = noteRepository;
    }

    public async Task<PatientNoteDto?> ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        var note = await _noteRepository.GetByIdAsync(id, cancellationToken);
        return note?.ToDto();
    }
}




