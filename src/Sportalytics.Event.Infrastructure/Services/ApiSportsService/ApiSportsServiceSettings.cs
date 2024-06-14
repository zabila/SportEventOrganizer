namespace Sportalytics.Event.Infrastructure.Services.ApiSportsService;

public class ApiSportsServiceSettings
{
    public string ApiKey { get; init; } = string.Empty;
    public string BaseUrl { get; init; } = string.Empty;
    public TimeSpan TimeToNextRun { get; set; }
    public TimeSpan TimeForRetry { get; set; }
}