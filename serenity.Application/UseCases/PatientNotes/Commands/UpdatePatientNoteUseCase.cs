using serenity.Application.DTOs;
using serenity.Application.Interfaces;
using serenity.Application.UseCases.PatientNotes;

namespace serenity.Application.UseCases.PatientNotes.Commands;

public class UpdatePatientNoteUseCase
{
    private readonly IPatientNoteRepository _noteRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePatientNoteUseCase(IPatientNoteRepository noteRepository, IUnitOfWork unitOfWork)
    {
        _noteRepository = noteRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<PatientNoteDto> ExecuteAsync(int id, UpdatePatientNoteRequest request, CancellationToken cancellationToken = default)
    {
        var note = await _noteRepository.GetByIdAsync(id, cancellationToken)
                  ?? throw new KeyNotFoundException($"No se encontró la nota con id {id}.");

        if (request.Date.HasValue)
        {
            note.Date = request.Date.Value;
        }

        if (request.Content is not null)
        {
            note.Content = request.Content;
        }

        if (request.Mood.HasValue)
        {
            note.Mood = request.Mood.Value;
        }

        if (request.AiDiagnosis is not null)
        {
            note.AiDiagnosis = request.AiDiagnosis;
        }

        if (request.NeedsFollowUp.HasValue)
        {
            note.NeedsFollowUp = request.NeedsFollowUp;
        }

        note.UpdatedAt = DateTime.Now;

        await _noteRepository.UpdateAsync(note);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return note.ToDto();
    }
}




