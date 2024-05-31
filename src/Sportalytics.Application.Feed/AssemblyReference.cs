using System.Reflection;

namespace Sportalytics.Application.Feed;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}