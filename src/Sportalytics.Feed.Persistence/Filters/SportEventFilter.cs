namespace Sportalytics.Feed.Persistence.Filters;

public record SportEventFilter
{
    public IReadOnlyCollection<Guid>? Ids { get; set; }
}