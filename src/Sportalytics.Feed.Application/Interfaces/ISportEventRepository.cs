using Sportalytics.Feed.Domain.Entities;

namespace Sportalytics.Feed.Application.Interfaces;

public interface ISportEventRepository
{
    Task<SportEvent?> GetByIdAsync(Guid id);

    Task AddAsync(SportEvent sportEvent, CancellationToken cancellationToken);
    
    Task<List<SportEvent>> GetAllAsync();
    
    Task UpdateAsync(Guid id, SportEvent sportEvent);
    
    Task DeleteByGuidAsync(Guid id);
}