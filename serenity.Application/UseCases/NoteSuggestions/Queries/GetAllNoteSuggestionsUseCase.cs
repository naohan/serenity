using serenity.Application.DTOs;
using serenity.Application.Interfaces;
using serenity.Application.UseCases.NoteSuggestions;

namespace serenity.Application.UseCases.NoteSuggestions.Queries;

public class GetAllNoteSuggestionsUseCase
{
    private readonly INoteSuggestionRepository _suggestionRepository;

    public GetAllNoteSuggestionsUseCase(INoteSuggestionRepository suggestionRepository)
    {
        _suggestionRepository = suggestionRepository;
    }

    public async Task<IEnumerable<NoteSuggestionDto>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var suggestions = await _suggestionRepository.GetAllAsync(cancellationToken);
        return suggestions.Select(s => s.ToDto());
    }
}



