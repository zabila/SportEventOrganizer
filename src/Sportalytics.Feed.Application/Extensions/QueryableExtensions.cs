using Sportalytics.Feed.Domain.Exceptions;

namespace Sportalytics.Feed.Application.Extensions;

public static class QueryableExtensions
{
    public static T EnsureFound<T>(this T? entity) where T : class?
    {
        if (entity is null)
        {
            throw new EntityNotFoundException();
        }

        return entity;
    }
}