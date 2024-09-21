namespace Sportalytics.Event.Infrastructure.Interfaces;

public interface IApiSportService
{
    Task DoWorkAsync(CancellationToken cancellationToken);
}