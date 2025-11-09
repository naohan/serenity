using serenity.Infrastructure;

namespace serenity.Application.Interfaces;

public interface IPsychologistRepository : IRepository<Psychologist>
{
    Task<Psychologist?> GetByUserIdAsync(int userId, CancellationToken cancellationToken = default);
    Task<Psychologist?> GetByCollegeNumberAsync(string collegeNumber, CancellationToken cancellationToken = default);
}

