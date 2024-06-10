using Sportalytics.Application.Interfaces;
using Sportalytics.Domain.Entities;
using Sportalytics.Persistence.Core;

namespace Sportalytics.Persistence.Repositories;

public class SportEventRepository(RepositoryContext repositoryContext) : RepositoryBase<SportEvent>(repositoryContext), ISportEventRepository
{
    public Task<SportEvent?> GetByIdAsync(Guid id)
    {
        return FindByCondition(sportEvent => sportEvent.Id == id);
    }
}