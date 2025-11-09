using Microsoft.EntityFrameworkCore;
using serenity.Application.Interfaces;
using serenity.Infrastructure.Data;
using serenity.Infrastructure;

namespace serenity.Infrastructure.Adapters.Repositories;

public class PatientRepository : Repository<Patient>, IPatientRepository
{
    public PatientRepository(SerenityDbContext context) : base(context)
    {
    }

    public Task<Patient?> GetByUserIdAsync(int userId, CancellationToken cancellationToken = default)
    {
        return DbSet.FirstOrDefaultAsync(p => p.UserId == userId, cancellationToken);
    }
}

