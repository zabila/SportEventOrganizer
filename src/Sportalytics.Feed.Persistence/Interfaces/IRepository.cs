namespace Sportalytics.Feed.Persistence.Interfaces;

public interface IRepository<T, in TFilter>
{
    IQueryable<T> Query(TFilter filter);
    Task AddAsync(T entity, CancellationToken cancellationToken = default);
    void Update(T entity);
    void Delete(T entity);
}