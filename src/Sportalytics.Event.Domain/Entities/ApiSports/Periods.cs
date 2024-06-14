using System.Text.Json.Serialization;

namespace Sportalytics.Event.Domain.Entities.ApiSports;

public class Periods
{
    [JsonPropertyName("first")]
    public int? First { get; set; }

    [JsonPropertyName("second")]
    public int? Second { get; set; }
}