using serenity.Application.DTOs;
using serenity.Application.Interfaces;
using serenity.Application.UseCases.NoteSuggestions;

namespace serenity.Application.UseCases.NoteSuggestions.Queries;

public class GetNoteSuggestionByIdUseCase
{
    private readonly INoteSuggestionRepository _suggestionRepository;

    public GetNoteSuggestionByIdUseCase(INoteSuggestionRepository suggestionRepository)
    {
        _suggestionRepository = suggestionRepository;
    }

    public async Task<NoteSuggestionDto?> ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        var suggestion = await _suggestionRepository.GetByIdAsync(id, cancellationToken);
        return suggestion?.ToDto();
    }
}



