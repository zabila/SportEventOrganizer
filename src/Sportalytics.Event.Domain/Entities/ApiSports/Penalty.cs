using Newtonsoft.Json;

namespace Sportalytics.Event.Domain.Entities.ApiSports;

[JsonObject(NamingStrategyType = typeof(LowerCaseNamingStrategy))]
public class Penalty
{
    public int? Home { get; set; }
    public int? Away { get; set; }
}