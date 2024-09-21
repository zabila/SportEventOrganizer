using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using Sportalytics.Event.Infrastructure.Interfaces;

namespace Sportalytics.Event.Infrastructure.Services.ApiSportsService;

public class BackgroundApiSportService(IServiceScopeFactory serviceScopeFactory) : IBackgroundApiSportService
{
    public void Start()
    {
        RecurringJob.AddOrUpdate<BackgroundApiSportService>(
            nameof(BackgroundApiSportService),
            x => x.DoWorkAsync(new CancellationToken()),
            Cron.Minutely);
    }

    public void Stop()
    {
        RecurringJob.RemoveIfExists(nameof(BackgroundApiSportService));
    }

    [AutomaticRetry(Attempts = 5)]
    public async Task DoWorkAsync(CancellationToken stoppingToken)
    {
        using var scope = serviceScopeFactory.CreateScope();
        var scopedProcessingService = scope.ServiceProvider.GetRequiredService<IApiSportService>();
        await scopedProcessingService.DoWorkAsync(stoppingToken);
    }
}