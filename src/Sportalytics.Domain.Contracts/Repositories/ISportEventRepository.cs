using Sportalytics.Domain.Entities;

namespace Sportalytics.Domain.Contracts.Repositories;

public interface ISportEventRepository
{
    Task<SportEvent?> GetByIdAsync(Guid id);

    Task AddAsync(SportEvent sportEvent, CancellationToken cancellationToken);
}