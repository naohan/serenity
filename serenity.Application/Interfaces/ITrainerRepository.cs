using serenity.Infrastructure;

namespace serenity.Application.Interfaces;

public interface ITrainerRepository : IRepository<Trainer>
{
    Task<Trainer?> GetByUserIdAsync(int userId, CancellationToken cancellationToken = default);
}

