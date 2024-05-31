namespace Sportalytics.Domain.Contracts.Repositories;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync();
    void BeginTransaction();
    Task CommitTransactionAsync();
    void RollbackTransaction();
}