using Microsoft.EntityFrameworkCore.Storage;

namespace Sportalytics.Feed.Persistence.PostgreSQL.Interfaces;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync();
    IDbContextTransaction BeginTransaction();
}