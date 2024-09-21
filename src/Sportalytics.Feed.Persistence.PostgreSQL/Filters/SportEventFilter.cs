namespace Sportalytics.Feed.Persistence.PostgreSQL.Filters;

public record SportEventFilter
{
    public IReadOnlyCollection<Guid>? Ids { get; set; }
}