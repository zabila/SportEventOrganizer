using Newtonsoft.Json;

namespace Sportalytics.Event.Domain.Entities.ApiSports;

[JsonObject(NamingStrategyType = typeof(LowercaseNamingStrategy))]
public class ExtraTime
{
    public int? Home { get; set; }
    public int? Away { get; set; }
}