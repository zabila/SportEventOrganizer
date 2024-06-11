using Microsoft.EntityFrameworkCore.Storage;

namespace Sportalytics.Feed.Application.Interfaces;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync();
    IDbContextTransaction GetTransaction();
}