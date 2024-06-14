using System.Text.Json.Serialization;

namespace Sportalytics.Event.Domain.Entities.ApiSports;

public class Status
{
    [JsonPropertyName("long")]
    public string? Long { get; set; }

    [JsonPropertyName("short")]
    public string? Short { get; set; }

    [JsonPropertyName("elapsed")]
    public int? Elapsed { get; set; }
}