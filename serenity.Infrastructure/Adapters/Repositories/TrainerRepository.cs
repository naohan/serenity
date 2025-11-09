using Microsoft.EntityFrameworkCore;
using serenity.Application.Interfaces;
using serenity.Infrastructure.Data;
using serenity.Infrastructure;

namespace serenity.Infrastructure.Adapters.Repositories;

public class TrainerRepository : Repository<Trainer>, ITrainerRepository
{
    public TrainerRepository(SerenityDbContext context) : base(context)
    {
    }

    public Task<Trainer?> GetByUserIdAsync(int userId, CancellationToken cancellationToken = default)
    {
        return DbSet.FirstOrDefaultAsync(t => t.UserId == userId, cancellationToken);
    }
}

