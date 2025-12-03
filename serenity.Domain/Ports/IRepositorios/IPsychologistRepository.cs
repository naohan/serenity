using serenity.Infrastructure;

namespace serenity.Domain.Ports.IRepositorios;

/// <summary>
/// Repository port for Psychologist domain entity.
/// </summary>
public interface IPsychologistRepository : IRepository<Psychologist>
{
    Task<Psychologist?> GetByUserIdAsync(int userId, CancellationToken cancellationToken = default);
    Task<Psychologist?> GetByCollegeNumberAsync(string collegeNumber, CancellationToken cancellationToken = default);
}



