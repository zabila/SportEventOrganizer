namespace Sportalytics.API.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        var presentationAssembly = typeof(Sportalytics.Presentation.AssemblyReference).Assembly;
        services.AddControllers()
            .AddApplicationPart(presentationAssembly);
        
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }
}