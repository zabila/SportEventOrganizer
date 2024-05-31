using System.Linq.Expressions;
using Sportalytics.Domain.Entities;

namespace Sportalytics.Domain.Contracts.Repositories;

public interface IRepositoryBase<T>
{
    IQueryable<T> FindAll();
    Task<T?> FindByCondition(Expression<Func<T, bool>> expression);

    Task AddAsync(T entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}