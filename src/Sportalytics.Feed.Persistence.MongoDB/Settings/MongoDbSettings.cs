namespace Sportalytics.Feed.Persistence.MongoDB.Settings;

public class MongoDbSettings
{
    public string? Host { get; init; }
    public int Port { get; init; }
    public string? DatabaseName { get; init; }
    public string? SportEventCollectionName { get; init; }

}