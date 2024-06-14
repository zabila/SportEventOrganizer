using Newtonsoft.Json;

namespace Sportalytics.Event.Domain.Entities.ApiSports;

[JsonObject(NamingStrategyType = typeof(LowercaseNamingStrategy))]
public class Venue
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public string? City { get; set; }
}