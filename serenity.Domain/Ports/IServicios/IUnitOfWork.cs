namespace serenity.Domain.Ports.IServicios;

/// <summary>
/// Unit of Work port for persisting changes in the data store.
/// </summary>
public interface IUnitOfWork
{
    void SaveChanges();
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}



