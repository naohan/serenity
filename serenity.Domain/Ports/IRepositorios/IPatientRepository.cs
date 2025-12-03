using serenity.Infrastructure;

namespace serenity.Domain.Ports.IRepositorios;

/// <summary>
/// Repository port for Patient domain entity.
/// </summary>
public interface IPatientRepository : IRepository<Patient>
{
    Task<Patient?> GetByUserIdAsync(int userId, CancellationToken cancellationToken = default);
}




