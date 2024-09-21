using Newtonsoft.Json;

namespace Sportalytics.Event.Domain.Entities.ApiSports;

[JsonObject(NamingStrategyType = typeof(LowerCaseNamingStrategy))]
public class Score
{
    public HalfTime? HalfTime { get; set; }
    public FullTime? FullTime { get; set; }
    public ExtraTime? ExtraTime { get; set; }
    public Penalty? Penalty { get; set; }
}