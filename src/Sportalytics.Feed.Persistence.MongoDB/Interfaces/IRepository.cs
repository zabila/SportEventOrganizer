﻿using System.Linq.Expressions;
using MongoDB.Driver;
using Sportalytics.Feed.Domain.Interfaces;

namespace Sportalytics.Feed.Persistence.MongoDB.Interfaces;

public interface IRepository<T> where T : IEntity
{
    IFindFluent<T, T> Query(Expression<Func<T, bool>> filter);

    Task AddAsync(T entity, CancellationToken cancellationToken);
    Task UpdateAsync(T entity, CancellationToken cancellationToken);
    Task DeleteAsync(T entity, CancellationToken cancellationToken);
}