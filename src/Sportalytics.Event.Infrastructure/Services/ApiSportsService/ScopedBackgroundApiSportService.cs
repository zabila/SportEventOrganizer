using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using Sportalytics.Event.Infrastructure.Interfaces;

namespace Sportalytics.Event.Infrastructure.Services.ApiSportsService;

public class ScopedBackgroundApiSportService(IServiceScopeFactory serviceScopeFactory) : IScopedBackgroundApiSportService
{
    public void Start()
    {
        RecurringJob.AddOrUpdate<ScopedBackgroundApiSportService>(
            nameof(ScopedBackgroundApiSportService),
            x => x.DoWorkAsync(new CancellationToken()),
            Cron.Minutely);
    }

    public void Stop()
    {
        RecurringJob.RemoveIfExists(nameof(ScopedBackgroundApiSportService));
    }

    [AutomaticRetry(Attempts = 5)]
    public async Task DoWorkAsync(CancellationToken stoppingToken)
    {
        using var scope = serviceScopeFactory.CreateScope();
        var scopedProcessingService = scope.ServiceProvider.GetRequiredService<IScopedApiSportService>();
        await scopedProcessingService.DoWorkAsync(stoppingToken);
    }
}