using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Sportalytics.Domain.Contracts.Repositories;

namespace Sportalytics.Infrastructure.Repository.Core;

public class UnitOfWork(RepositoryContext context) : IUnitOfWork
{
    private readonly RepositoryContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public IDbContextTransaction GetTransaction()
    {
        return _context.Database.BeginTransaction();
    }

    public void Dispose()
    {
        _context?.Dispose();
    }
}