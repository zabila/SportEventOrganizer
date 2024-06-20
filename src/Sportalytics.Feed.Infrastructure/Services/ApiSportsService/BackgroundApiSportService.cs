using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using Sportalytics.Feed.Infrastructure.Interfaces;
using Sportalytics.Feed.Infrastructure.Kafka.Consumer;
using Sportalytics.Feed.Infrastructure.Kafka.Interfaces;

namespace Sportalytics.Feed.Infrastructure.Services.ApiSportsService;

public class BackgroundApiSportService(IServiceScopeFactory serviceScopeFactory) : IBackgroundApiSportService
{
    private const string Topic = "api-sports";
    private string JobId { get; set; } = string.Empty;

    public void Start()
    {
        JobId = BackgroundJob.Enqueue<BackgroundApiSportService>(x => x.StartConsumeAsync(new CancellationToken()));
    }

    public void Stop()
    {
        BackgroundJob.Delete(JobId);
    }

    [AutomaticRetry(Attempts = 5)]
    public async Task StartConsumeAsync(CancellationToken cancellationToken)
    {
        using var scope = serviceScopeFactory.CreateScope();
        var scopedProcessingService = scope.ServiceProvider.GetRequiredService<IKafkaConsumer<string, string>>();
        await scopedProcessingService.ConsumeAsync(Topic, cancellationToken);
    }
}