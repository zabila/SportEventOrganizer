using Newtonsoft.Json.Serialization;

namespace Sportalytics.Event.Domain.Entities.ApiSports;

public class LowercaseNamingStrategy : NamingStrategy
{
    protected override string ResolvePropertyName(string name)
    {
        return name.ToLower();
    }
}