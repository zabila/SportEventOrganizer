using Newtonsoft.Json;

namespace Sportalytics.Event.Domain.Entities.ApiSports;

[JsonObject(NamingStrategyType = typeof(LowerCaseNamingStrategy))]
public class Fixture
{
    public int Id { get; set; }
    public string? Referee { get; set; }
    public string? Timezone { get; set; }
    public DateTime Date { get; set; }
    public int Timestamp { get; set; }
    public Periods? Periods { get; set; }
    public Venue? Venue { get; set; }
    public Status? Status { get; set; }
}