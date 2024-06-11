namespace Sportalytics.Feed.Application.Interfaces;

public interface IRepositoryManager
{
    ISportEventRepository SportEvents { get; }
}