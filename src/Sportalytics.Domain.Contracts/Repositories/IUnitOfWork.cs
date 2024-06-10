using Microsoft.EntityFrameworkCore.Storage;

namespace Sportalytics.Domain.Contracts.Repositories;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync();
    IDbContextTransaction GetTransaction();
}