namespace serenity.Application.Interfaces;

/// <summary>
/// Unit of Work abstraction to persist changes in the Serenity data store.
/// </summary>
public interface IUnitOfWork
{
    void SaveChanges();
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}

