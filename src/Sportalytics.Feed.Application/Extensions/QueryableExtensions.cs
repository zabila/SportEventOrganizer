using Sportalytics.Feed.Domain.Exceptions;

namespace Sportalytics.Feed.Application.Extensions;

public static class QueryableExtensions
{
    public async static Task<T> EnsureFound<T>(this Task<T> task) where T : class?
    {
        var result = await task;
        if (result == null)
        {
            throw new EntityNotFoundException();
        }

        return result;
    }
}