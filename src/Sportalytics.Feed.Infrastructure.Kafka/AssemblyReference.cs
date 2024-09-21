using System.Reflection;

namespace Sportalytics.Feed.Infrastructure.Kafka;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}