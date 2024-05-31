using Sportalytics.Domain.Contracts.Repositories;
using Sportalytics.Domain.Entities;
using Sportalytics.Infrastructure.Repository.Core;

namespace Sportalytics.Infrastructure.Repository.Repositories;

public class SportEventRepository(RepositoryContext repositoryContext) : RepositoryBase<SportEvent>(repositoryContext), ISportEventRepository
{
    public Task<SportEvent?> GetByIdAsync(Guid id)
    {
        return FindByCondition(sportEvent => sportEvent.Id == id);
    }
}