using System.Linq.Expressions;

namespace serenity.Domain.Ports.IRepositorios;

/// <summary>
/// Generic repository contract for domain entities.
/// </summary>
public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
}

