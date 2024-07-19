using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Sportalytics.Feed.Domain.Entities;
using Sportalytics.Feed.Persistence.MongoDB.Interfaces;
using Xunit;

namespace Sportalytics.Feed.Test.Integration;

public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestAppFactory>
{
    protected readonly HttpClient Client;
    protected readonly IRepository<SportEvent> SportEventRepository;

    protected BaseIntegrationTest(IntegrationTestAppFactory factory)
    {
        var scope = factory.Services.CreateScope();
        Client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
        SportEventRepository = scope.ServiceProvider.GetRequiredService<IRepository<SportEvent>>();
    }
}