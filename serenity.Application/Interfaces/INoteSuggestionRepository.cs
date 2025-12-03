using serenity.Infrastructure;

namespace serenity.Application.Interfaces;

public interface INoteSuggestionRepository : IRepository<NoteSuggestion>
{
    Task<IEnumerable<NoteSuggestion>> GetByNoteIdAsync(int noteId, CancellationToken cancellationToken = default);
}




