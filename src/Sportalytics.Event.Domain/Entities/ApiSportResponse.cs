using System.Text.Json.Serialization;
using Sportalytics.Event.Domain.Entities.ApiSports;

namespace Sportalytics.Event.Domain.Entities;

public class ApiSportResponse
{
    [JsonPropertyName("get")]
    public string? Get { get; set; }

    [JsonPropertyName("parameters")]
    public Parameters? Parameters { get; set; }

    [JsonPropertyName("errors")]
    public ErrorModel? Errors { get; set; }

    [JsonPropertyName("results")]
    public int Results { get; set; }

    [JsonPropertyName("paging")]
    public Paging? Paging { get; set; }

    [JsonPropertyName("response")]
    public List<Response>? Response { get; set; }
}