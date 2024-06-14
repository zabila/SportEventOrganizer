using Newtonsoft.Json;

namespace Sportalytics.Event.Domain.Entities.ApiSports;

[JsonObject(NamingStrategyType = typeof(LowercaseNamingStrategy))]
public class Parameters
{
    public string? Date { get; set; }
    public string? Status { get; set; }
}