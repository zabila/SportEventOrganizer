using Sportalytics.Application.Interfaces;
using Sportalytics.Persistence.Repositories;

namespace Sportalytics.Persistence.Core;

public sealed class RepositoryManager(RepositoryContext repositoryContext) : IRepositoryManager
{
    private readonly Lazy<ISportEventRepository> _sportEvents = new(() => new SportEventRepository(repositoryContext));

    public ISportEventRepository SportEvents => _sportEvents.Value;
}