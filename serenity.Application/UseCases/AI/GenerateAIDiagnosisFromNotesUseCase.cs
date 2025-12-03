using serenity.Application.DTOs;
using serenity.Application.Interfaces;

using serenity.Infrastructure;

namespace serenity.Application.UseCases.AI;

public class GenerateAIDiagnosisFromNotesUseCase
{
    private readonly IAssistantService _assistantService;
    private readonly IPatientNoteRepository _noteRepository;
    private readonly INoteSuggestionRepository _suggestionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public GenerateAIDiagnosisFromNotesUseCase(
        IAssistantService assistantService,
        IPatientNoteRepository noteRepository,
        INoteSuggestionRepository suggestionRepository,
        IUnitOfWork unitOfWork)
    {
        _assistantService = assistantService;
        _noteRepository = noteRepository;
        _suggestionRepository = suggestionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<DiagnosisResponseDto> ExecuteAsync(int noteId, CancellationToken cancellationToken = default)
    {
        var note = await _noteRepository.GetByIdAsync(noteId, cancellationToken)
                  ?? throw new KeyNotFoundException($"No se encontró la nota con id {noteId}.");

        var result = await _assistantService.AnalyzeNotesAsync(noteId, cancellationToken);

        // Guardar sugerencias en la base de datos
        var now = DateTime.Now;
        foreach (var suggestion in result.Suggestions)
        {
            var suggestionEntity = new NoteSuggestion
            {
                NoteId = noteId,
                Suggestion = suggestion,
                CreatedAt = now
            };
            await _suggestionRepository.AddAsync(suggestionEntity, cancellationToken);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new DiagnosisResponseDto
        {
            NoteId = noteId,
            Diagnosis = result.Diagnosis,
            Suggestions = result.Suggestions,
            Analysis = result.Analysis,
            GeneratedAt = now
        };
    }
}

