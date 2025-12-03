using serenity.Application.DTOs;
using serenity.Application.Interfaces;
using serenity.Application.UseCases.NoteSuggestions;
using serenity.Infrastructure;

namespace serenity.Application.UseCases.NoteSuggestions.Commands;

public class CreateNoteSuggestionUseCase
{
    private readonly INoteSuggestionRepository _suggestionRepository;
    private readonly IPatientNoteRepository _noteRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateNoteSuggestionUseCase(
        INoteSuggestionRepository suggestionRepository,
        IPatientNoteRepository noteRepository,
        IUnitOfWork unitOfWork)
    {
        _suggestionRepository = suggestionRepository;
        _noteRepository = noteRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<NoteSuggestionDto> ExecuteAsync(CreateNoteSuggestionRequest request, CancellationToken cancellationToken = default)
    {
        var note = await _noteRepository.GetByIdAsync(request.NoteId, cancellationToken);
        if (note is null)
        {
            throw new KeyNotFoundException($"No se encontró la nota con id {request.NoteId}.");
        }

        var suggestion = new NoteSuggestion
        {
            NoteId = request.NoteId,
            Suggestion = request.Suggestion,
            CreatedAt = DateTime.Now
        };

        await _suggestionRepository.AddAsync(suggestion, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return suggestion.ToDto();
    }
}




