using System.Text.Json.Serialization;

namespace Sportalytics.Event.Domain.Entities.ApiSports;

public class Parameters
{
    [JsonPropertyName("date")]
    public string? Date { get; set; }

    [JsonPropertyName("status")]
    public string? Status { get; set; }
}