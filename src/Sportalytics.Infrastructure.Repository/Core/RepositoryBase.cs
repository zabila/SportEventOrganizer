using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Sportalytics.Domain.Contracts.Repositories;

namespace Sportalytics.Infrastructure.Repository.Core;

public class RepositoryBase<T>(RepositoryContext repositoryContext) : IRepositoryBase<T> where T : class
{
    public IQueryable<T> FindAll()
    {
        return repositoryContext.Set<T>();
    }

    public Task<T?> FindByCondition(Expression<Func<T, bool>> expression)
    {
        return repositoryContext.Set<T>().FirstOrDefaultAsync(expression)!;
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken)
    {
        await repositoryContext.Set<T>().AddAsync(entity, cancellationToken);
    }

    public Task UpdateAsync(T entity)
    {
        repositoryContext.Set<T>().Update(entity);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(T entity)
    {
        repositoryContext.Set<T>().Remove(entity);
        return Task.CompletedTask;
    }
}