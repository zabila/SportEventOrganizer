using Microsoft.EntityFrameworkCore.Storage;
using Sportalytics.Feed.Domain.Entities;
using Sportalytics.Feed.Persistence.Filters;
using Sportalytics.Feed.Persistence.Interfaces;
using Sportalytics.Feed.Persistence.Repositories;

namespace Sportalytics.Feed.Persistence.Core;

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