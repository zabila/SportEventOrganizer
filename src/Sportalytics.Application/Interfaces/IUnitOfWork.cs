using Microsoft.EntityFrameworkCore.Storage;

namespace Sportalytics.Application.Interfaces;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync();
    IDbContextTransaction GetTransaction();
}