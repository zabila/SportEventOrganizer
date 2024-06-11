using Microsoft.EntityFrameworkCore.Storage;
using Sportalytics.Feed.Application.Interfaces;

namespace Sportalytics.Feed.Persistence.Core;

public class UnitOfWork(RepositoryContext context) : IUnitOfWork
{
    private readonly RepositoryContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public IDbContextTransaction BeginTransaction()
    {
        return _context.Database.BeginTransaction();
    }

    public void Dispose()
    {
        _context?.Dispose();
    }
}