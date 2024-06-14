using Microsoft.EntityFrameworkCore.Storage;
using Sportalytics.Feed.Domain.Entities;
using Sportalytics.Feed.Persistence.PostgreSQL.Filters;
using Sportalytics.Feed.Persistence.PostgreSQL.Interfaces;
using Sportalytics.Feed.Persistence.PostgreSQL.Repositories;

namespace Sportalytics.Feed.Persistence.PostgreSQL.Core;

public sealed class RepositoryManager(FeedServiceContext feedServiceContext) : IRepositoryManager
{
    private readonly Lazy<IRepository<SportEvent, SportEventFilter>> _sportEvents = new(() => new SportEventRepository(feedServiceContext));

    public IRepository<SportEvent, SportEventFilter> SportEvents => _sportEvents.Value;

    public async Task<int> SaveChangesAsync()
    {
        return await feedServiceContext.SaveChangesAsync();
    }

    public IDbContextTransaction BeginTransaction()
    {
        return feedServiceContext.Database.BeginTransaction();
    }

    public void Dispose()
    {
        feedServiceContext?.Dispose();
    }
}