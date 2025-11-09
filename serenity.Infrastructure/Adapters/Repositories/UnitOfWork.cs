using serenity.Application.Interfaces;
using serenity.Infrastructure.Data;

namespace serenity.Infrastructure.Adapters.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly SerenityDbContext _context;

    public UnitOfWork(SerenityDbContext context)
    {
        _context = context;
    }

    public void SaveChanges()
    {
        _context.SaveChanges();
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}

