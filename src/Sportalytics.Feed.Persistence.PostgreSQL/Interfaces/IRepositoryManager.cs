using Sportalytics.Feed.Domain.Entities;
using Sportalytics.Feed.Persistence.PostgreSQL.Filters;

namespace Sportalytics.Feed.Persistence.PostgreSQL.Interfaces;

public interface IRepositoryManager : IUnitOfWork
{
    IRepository<SportEvent, SportEventFilter> SportEvents { get; }
}