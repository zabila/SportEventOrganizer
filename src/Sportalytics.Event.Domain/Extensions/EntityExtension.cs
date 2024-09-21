namespace Sportalytics.Event.Domain.Extensions;

public static class EntityExtension
{
    public static T EnsureExists<T>(this T? obj) where T : class?
    {
        ArgumentNullException.ThrowIfNull(obj);
        return obj;
    }

    public static string EnsureExists(this string? obj)
    {
        ArgumentException.ThrowIfNullOrEmpty(obj);
        return obj;
    }
}