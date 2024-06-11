using Sportalytics.Feed.Application.Interfaces;
using Sportalytics.Feed.Domain.Entities;
using Sportalytics.Feed.Persistence.Core;

namespace Sportalytics.Feed.Persistence.Repositories;

public class SportEventRepository(RepositoryContext repositoryContext) : RepositoryBase<SportEvent>(repositoryContext), ISportEventRepository
{
    public Task<SportEvent?> GetByIdAsync(Guid id)
    {
        return FindByCondition(sportEvent => sportEvent.Id == id);
    }
}