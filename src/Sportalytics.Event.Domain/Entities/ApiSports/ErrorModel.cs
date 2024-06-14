using System.Text.Json.Serialization;

namespace Sportalytics.Event.Domain.Entities.ApiSports;

public class ErrorModel
{
    [JsonPropertyName("token")]
    public string? Token { get; set; }
}