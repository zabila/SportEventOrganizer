using Sportalytics.Domain.Contracts.Repositories;
using Sportalytics.Infrastructure.Repository.Repositories;

namespace Sportalytics.Infrastructure.Repository.Core;

public sealed class RepositoryManager(RepositoryContext repositoryContext) : IRepositoryManager
{
    private readonly Lazy<ISportEventRepository> _sportEvents = new(() => new SportEventRepository(repositoryContext));

    public ISportEventRepository SportEvents => _sportEvents.Value;
}