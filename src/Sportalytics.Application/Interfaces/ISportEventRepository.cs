using Sportalytics.Domain.Entities;

namespace Sportalytics.Application.Interfaces;

public interface ISportEventRepository
{
    Task<SportEvent?> GetByIdAsync(Guid id);

    Task AddAsync(SportEvent sportEvent, CancellationToken cancellationToken);
}