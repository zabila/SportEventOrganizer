using Newtonsoft.Json;

namespace Sportalytics.Event.Domain.Entities.ApiSports;

[JsonObject(NamingStrategyType = typeof(LowercaseNamingStrategy))]
public class Status
{
    public string? Long { get; set; }
    public string? Short { get; set; }
    public int? Elapsed { get; set; }
}