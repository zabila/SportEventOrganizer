using System.Text.Json.Serialization;

namespace Sportalytics.Event.Domain.Entities.ApiSports;

public class Penalty
{
    [JsonPropertyName("home")]
    public int? Home { get; set; }

    [JsonPropertyName("away")]
    public int? Away { get; set; }
}