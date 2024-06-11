namespace Sportalytics.Feed.API.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        var presentationAssembly = typeof(Presentation.AssemblyReference).Assembly;
        services.AddControllers()
            .AddApplicationPart(presentationAssembly);
        
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }
}