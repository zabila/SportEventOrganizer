using Newtonsoft.Json;

namespace Sportalytics.Event.Domain.Entities.ApiSports;

[JsonObject(NamingStrategyType = typeof(LowercaseNamingStrategy))]
public class Response
{
    public Fixture? Fixture { get; set; }
    public League? League { get; set; }
    public Teams? Teams { get; set; }
    public Goals? Goals { get; set; }
    public Score? Score { get; set; }
}