using Hangfire;

namespace Sportalytics.Feed.API.Extensions;

public static class HandfireAppExtensions
{
    public static void ConfigureHandfireDashboard(this IApplicationBuilder app)
    {
        app.UseHangfireDashboard("/hangfire");
    }
}