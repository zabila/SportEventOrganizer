using Microsoft.EntityFrameworkCore;
using Sportalytics.Feed.Application.Interfaces;
using Sportalytics.Feed.Domain.Entities;
using Sportalytics.Feed.Persistence.Core;
using Sportalytics.Feed.Persistence.Filters;

namespace Sportalytics.Feed.Persistence.Repositories;

public class SportEventRepository(FeedServiceContext feedServiceContext) : RepositoryBase<SportEvent, SportEventFilter>(feedServiceContext)
{
    private readonly FeedServiceContext _feedServiceContext = feedServiceContext;

    public override IQueryable<SportEvent> Query(SportEventFilter filter)
    {
        IQueryable<SportEvent> query = _feedServiceContext.SportEvents;
        if (filter.Ids != null && filter.Ids.Count != 0)
        {
            query = query.Where(x => filter.Ids.Contains(x.Id));
        }

        return query;
    }
}