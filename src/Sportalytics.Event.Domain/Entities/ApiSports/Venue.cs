using Newtonsoft.Json;

namespace Sportalytics.Event.Domain.Entities.ApiSports;

[JsonObject(NamingStrategyType = typeof(LowerCaseNamingStrategy))]
public class Venue
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public string? City { get; set; }
}