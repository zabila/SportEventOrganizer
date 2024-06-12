using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Sportalytics.Feed.Application.Interfaces;
using Sportalytics.Feed.Persistence.Interfaces;

namespace Sportalytics.Feed.Persistence.Core;

public abstract class RepositoryBase<T, TFilter>(FeedServiceContext feedServiceContext) : IRepository<T, TFilter>
    where T : class
    where TFilter : class
{
    public abstract IQueryable<T> Query(TFilter filter);

    public async Task AddAsync(T entity, CancellationToken cancellationToken)
    {
        await feedServiceContext.Set<T>().AddAsync(entity, cancellationToken);
    }

    public void Update(T entity)
    {
        feedServiceContext.Set<T>().Update(entity);
    }

    public void Delete(T entity)
    {
        feedServiceContext.Set<T>().Remove(entity);
    }
}