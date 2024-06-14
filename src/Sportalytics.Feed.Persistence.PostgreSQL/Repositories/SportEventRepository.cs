using Sportalytics.Feed.Domain.Entities;
using Sportalytics.Feed.Persistence.PostgreSQL.Core;
using Sportalytics.Feed.Persistence.PostgreSQL.Filters;

namespace Sportalytics.Feed.Persistence.PostgreSQL.Repositories;

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