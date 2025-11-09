using Microsoft.EntityFrameworkCore;
using serenity.Application.Interfaces;
using serenity.Infrastructure.Data;
using serenity.Infrastructure;

namespace serenity.Infrastructure.Adapters.Repositories;

public class PsychologistRepository : Repository<Psychologist>, IPsychologistRepository
{
    public PsychologistRepository(SerenityDbContext context) : base(context)
    {
    }

    public Task<Psychologist?> GetByUserIdAsync(int userId, CancellationToken cancellationToken = default)
    {
        return DbSet.FirstOrDefaultAsync(p => p.UserId == userId, cancellationToken);
    }

    public Task<Psychologist?> GetByCollegeNumberAsync(string collegeNumber, CancellationToken cancellationToken = default)
    {
        return DbSet.FirstOrDefaultAsync(p => p.CollegeNumber == collegeNumber, cancellationToken);
    }
}

