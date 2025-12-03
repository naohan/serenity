using serenity.Application.Interfaces;

namespace serenity.Application.UseCases.PatientNotes.Commands;

public class DeletePatientNoteUseCase
{
    private readonly IPatientNoteRepository _noteRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeletePatientNoteUseCase(IPatientNoteRepository noteRepository, IUnitOfWork unitOfWork)
    {
        _noteRepository = noteRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        var note = await _noteRepository.GetByIdAsync(id, cancellationToken)
                  ?? throw new KeyNotFoundException($"No se encontró la nota con id {id}.");

        await _noteRepository.DeleteAsync(note);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}



