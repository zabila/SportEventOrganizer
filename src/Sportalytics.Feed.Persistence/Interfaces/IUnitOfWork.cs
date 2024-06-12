using Microsoft.EntityFrameworkCore.Storage;

namespace Sportalytics.Feed.Persistence.Interfaces;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync();
    IDbContextTransaction BeginTransaction();
}