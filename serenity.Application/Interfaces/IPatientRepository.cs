using serenity.Infrastructure;

namespace serenity.Application.Interfaces;

public interface IPatientRepository : IRepository<Patient>
{
    Task<Patient?> GetByUserIdAsync(int userId, CancellationToken cancellationToken = default);
}

