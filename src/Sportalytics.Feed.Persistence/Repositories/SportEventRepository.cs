using Microsoft.EntityFrameworkCore;
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

    public async Task<List<SportEvent>> GetAllAsync()
    {
        return await FindAll().ToListAsync();
    }

    public async Task UpdateAsync(Guid id, SportEvent sportEvent)
    {
        await UpdateAsync(sportEvent);
    }

    public async Task DeleteByGuidAsync(Guid id)
    {
         var sportEvent = await GetByIdAsync(id);
         //NOTE: Pay attention. I'm not sure if this is the correct way to handle this.
         await DeleteAsync(sportEvent!);
    }
}