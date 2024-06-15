using Newtonsoft.Json;

namespace Sportalytics.Event.Domain.Entities.ApiSports;

[JsonObject(NamingStrategyType = typeof(LowerCaseNamingStrategy))]
public class ApiSportResponse
{
    public string? Get { get; set; }
    public Parameters? Parameters { get; set; }
    public ErrorModel? Errors { get; set; }
    public int Results { get; set; }
    public Paging? Paging { get; set; }
    public List<Response>? Response { get; set; }
}