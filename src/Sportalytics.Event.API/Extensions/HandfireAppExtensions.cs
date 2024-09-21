using Hangfire;

namespace Sportalytics.Event.API.Extensions;

public static class HandfireAppExtensions
{
    public static void ConfigureHandfireDashboard(this IApplicationBuilder app)
    {
        app.UseHangfireDashboard("/hangfire");
    }
}