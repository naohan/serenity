using serenity.Infrastructure;

namespace serenity.Domain.Ports.IRepositorios;

/// <summary>
/// Repository port for Trainer domain entity.
/// </summary>
public interface ITrainerRepository : IRepository<Trainer>
{
    Task<Trainer?> GetByUserIdAsync(int userId, CancellationToken cancellationToken = default);
}



