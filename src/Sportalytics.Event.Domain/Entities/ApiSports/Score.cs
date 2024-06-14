using System.Text.Json.Serialization;

namespace Sportalytics.Event.Domain.Entities.ApiSports;

public class Score
{
    [JsonPropertyName("halftime")]
    public HalfTime? HalfTime { get; set; }

    [JsonPropertyName("fulltime")]
    public FullTime? FullTime { get; set; }

    [JsonPropertyName("extratime")]
    public ExtraTime? ExtraTime { get; set; }

    [JsonPropertyName("penalty")]
    public Penalty? Penalty { get; set; }
}