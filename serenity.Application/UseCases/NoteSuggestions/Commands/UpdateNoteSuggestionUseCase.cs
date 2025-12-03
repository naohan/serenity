using serenity.Application.DTOs;
using serenity.Application.Interfaces;
using serenity.Application.UseCases.NoteSuggestions;

namespace serenity.Application.UseCases.NoteSuggestions.Commands;

public class UpdateNoteSuggestionUseCase
{
    private readonly INoteSuggestionRepository _suggestionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateNoteSuggestionUseCase(INoteSuggestionRepository suggestionRepository, IUnitOfWork unitOfWork)
    {
        _suggestionRepository = suggestionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<NoteSuggestionDto> ExecuteAsync(int id, UpdateNoteSuggestionRequest request, CancellationToken cancellationToken = default)
    {
        var suggestion = await _suggestionRepository.GetByIdAsync(id, cancellationToken)
                        ?? throw new KeyNotFoundException($"No se encontró la sugerencia con id {id}.");

        if (request.Suggestion is not null)
        {
            suggestion.Suggestion = request.Suggestion;
        }

        await _suggestionRepository.UpdateAsync(suggestion);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return suggestion.ToDto();
    }
}



