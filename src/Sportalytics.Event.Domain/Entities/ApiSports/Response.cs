using System.Text.Json.Serialization;

namespace Sportalytics.Event.Domain.Entities.ApiSports;

public class Response
{
    [JsonPropertyName("fixture")]
    public Fixture? Fixture { get; set; }

    [JsonPropertyName("league")]
    public League? League { get; set; }

    [JsonPropertyName("teams")]
    public Teams? Teams { get; set; }

    [JsonPropertyName("goals")]
    public Goals? Goals { get; set; }

    [JsonPropertyName("score")]
    public Score? Score { get; set; }
}