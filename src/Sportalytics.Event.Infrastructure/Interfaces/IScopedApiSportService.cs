using System.Text.Json;

namespace Sportalytics.Event.Infrastructure.Interfaces;

public interface IScopedApiSportService
{
    Task DoWorkAsync(CancellationToken stoppingToken);
}