using Sportalytics.Feed.Domain.Entities;
using Sportalytics.Feed.Persistence.Filters;

namespace Sportalytics.Feed.Persistence.Interfaces;

public interface IRepositoryManager : IUnitOfWork
{
    IRepository<SportEvent, SportEventFilter> SportEvents { get; }
}