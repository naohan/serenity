using serenity.Infrastructure;

namespace serenity.Domain.Ports.IRepositorios;

/// <summary>
/// Repository port for NoteSuggestion domain entity.
/// </summary>
public interface INoteSuggestionRepository : IRepository<NoteSuggestion>
{
    Task<IEnumerable<NoteSuggestion>> GetByNoteIdAsync(int noteId, CancellationToken cancellationToken = default);
}




