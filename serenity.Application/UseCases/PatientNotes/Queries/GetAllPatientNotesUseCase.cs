using serenity.Application.DTOs;
using serenity.Application.Interfaces;
using serenity.Application.UseCases.PatientNotes;

namespace serenity.Application.UseCases.PatientNotes.Queries;

public class GetAllPatientNotesUseCase
{
    private readonly IPatientNoteRepository _noteRepository;

    public GetAllPatientNotesUseCase(IPatientNoteRepository noteRepository)
    {
        _noteRepository = noteRepository;
    }

    public async Task<IEnumerable<PatientNoteDto>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var notes = await _noteRepository.GetAllAsync(cancellationToken);
        return notes.Select(n => n.ToDto());
    }
}




