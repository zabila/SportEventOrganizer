using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Sportalytics.Domain.Contracts.Repositories;

namespace Sportalytics.Infrastructure.Repository.Core;

public class UnitOfWork(RepositoryContext context) : IUnitOfWork
{
    private readonly RepositoryContext _context = context ?? throw new ArgumentNullException(nameof(context));
    private IDbContextTransaction? _transaction;

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void BeginTransaction()
    {
        _transaction = _context.Database.BeginTransaction();

    }

    public async Task CommitTransactionAsync()
    {
        try
        {
            await _context.SaveChangesAsync();
            await _transaction!.CommitAsync();
        }
        catch
        {
            await RollbackTransactionAsync();
            throw;
        }
        finally
        {
            _transaction!.Dispose();
            _transaction = null;
        }
        ;
    }

    public void RollbackTransaction()
    {
        try
        {
            _transaction!.Rollback();
        }
        finally
        {
            _transaction!.Dispose();
            _transaction = null;
        }
    }

    private async Task RollbackTransactionAsync()
    {
        try
        {
            await _transaction!.RollbackAsync();
        }
        finally
        {
            _transaction!.Dispose();
            _transaction = null;
        }
    }

    public void Dispose()
    {
        _context?.Dispose();
        _transaction?.Dispose();
    }
}