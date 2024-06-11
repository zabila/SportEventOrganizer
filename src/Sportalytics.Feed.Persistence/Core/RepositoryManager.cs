using Sportalytics.Feed.Application.Interfaces;
using Sportalytics.Feed.Persistence.Repositories;

namespace Sportalytics.Feed.Persistence.Core;

public sealed class RepositoryManager(RepositoryContext repositoryContext) : IRepositoryManager
{
    private readonly Lazy<ISportEventRepository> _sportEvents = new(() => new SportEventRepository(repositoryContext));

    public ISportEventRepository SportEvents => _sportEvents.Value;
}