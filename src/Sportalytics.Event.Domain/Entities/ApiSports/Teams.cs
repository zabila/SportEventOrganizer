using System.Text.Json.Serialization;

namespace Sportalytics.Event.Domain.Entities.ApiSports;

public class Teams
{
    [JsonPropertyName("home")]
    public Team? Home { get; set; }

    [JsonPropertyName("away")]
    public Team? Away { get; set; }
}