using serenity.Infrastructure;

namespace serenity.Application.Interfaces;

/// <summary>
/// Repository abstraction specialised for Serenity users.
/// </summary>
public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<User?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
}

