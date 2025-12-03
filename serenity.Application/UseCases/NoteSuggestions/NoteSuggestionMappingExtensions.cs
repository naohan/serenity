using serenity.Application.DTOs;
using serenity.Infrastructure;

namespace serenity.Application.UseCases.NoteSuggestions;

internal static class NoteSuggestionMappingExtensions
{
    public static NoteSuggestionDto ToDto(this NoteSuggestion suggestion)
    {
        return new NoteSuggestionDto
        {
            Id = suggestion.Id,
            NoteId = suggestion.NoteId,
            Suggestion = suggestion.Suggestion,
            CreatedAt = suggestion.CreatedAt
        };
    }
}




