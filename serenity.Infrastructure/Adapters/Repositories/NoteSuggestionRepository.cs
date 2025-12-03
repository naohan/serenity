using Microsoft.EntityFrameworkCore;
using serenity.Application.Interfaces;
using serenity.Infrastructure.Data;
using serenity.Infrastructure;

namespace serenity.Infrastructure.Adapters.Repositories;

public class NoteSuggestionRepository : Repository<NoteSuggestion>, INoteSuggestionRepository
{
    public NoteSuggestionRepository(SerenityDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<NoteSuggestion>> GetByNoteIdAsync(int noteId, CancellationToken cancellationToken = default)
    {
        return await DbSet.Where(s => s.NoteId == noteId)
            .OrderByDescending(s => s.CreatedAt)
            .ToListAsync(cancellationToken);
    }
}




