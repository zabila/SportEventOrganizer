namespace Sportalytics.Event.Infrastructure.Interfaces;

public interface IScopedBackgroundApiSportService
{
    void Start();
    void Stop();
}