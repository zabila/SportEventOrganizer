namespace Sportalytics.Domain.Contracts.Repositories;

public interface IRepositoryManager
{
    ISportEventRepository SportEvents { get; }
}