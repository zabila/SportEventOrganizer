namespace Sportalytics.Event.API.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        var presentationAssembly = typeof(Presentation.AssemblyReference).Assembly;
        services.AddControllers()
            .AddApplicationPart(presentationAssembly);
        return services;
    }
}