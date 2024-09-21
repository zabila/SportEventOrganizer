using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Sportalytics.Feed.Persistence.MongoDB.Settings;
using Testcontainers.MongoDb;
using Testcontainers.PostgreSql;

namespace Sportalytics.Feed.Test.Integration;

public class IntegrationTestAppFactory : WebApplicationFactory<Program>
{
    private readonly MongoDbContainer _mongoDbContainer = new MongoDbBuilder()
        .WithImage("mongo:latest")
        .WithPortBinding(27017, true)
        .WithEnvironment("MONGO_INITDB_ROOT_USERNAME", "root")
        .WithEnvironment("MONGO_INITDB_ROOT_PASSWORD", "example")
        .WithCleanUp(true)
        .Build();

    private readonly PostgreSqlContainer _pgsqlContainer = new PostgreSqlBuilder()
        .WithImage("postgres:latest")
        .WithDatabase("FeedServiceHangfire")
        .WithUsername("postgres")
        .WithPassword("postgres")
        .WithPortBinding(5432, true)
        .Build();

    public IntegrationTestAppFactory()
    {
        _mongoDbContainer.StartAsync().Wait();
        _pgsqlContainer.StartAsync().Wait();
    }


    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {

        var mongoDbSettings = new MongoDbSettings
        {
            Host = _mongoDbContainer.Hostname,
            Port = _mongoDbContainer.GetMappedPublicPort(27017),
            DatabaseName = "IntegrationTestDb",
            SportEventCollectionName = "SportEventCollectionTest",
            Username = "root",
            Password = "example"
        };

        var inMemorySettings = new Dictionary<string, string>
            {
                { "MongoDbSettings:Host", mongoDbSettings.Host },
                { "MongoDbSettings:Port", mongoDbSettings.Port.ToString() },
                { "MongoDbSettings:DatabaseName", mongoDbSettings.DatabaseName },
                { "MongoDbSettings:Username", mongoDbSettings.Username },
                { "MongoDbSettings:Password", mongoDbSettings.Password},
                { "MongoDbSettings:SportEventCollectionName", mongoDbSettings.SportEventCollectionName },
                { "ConnectionStrings:DefaultHangfireConnection", _pgsqlContainer.GetConnectionString() }
            };

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings.Select(kv => new KeyValuePair<string, string?>(kv.Key, kv.Value)))
            .Build();

        builder
        .UseConfiguration(configuration)
        .ConfigureAppConfiguration((context, config) =>
        {
            config.AddInMemoryCollection(inMemorySettings.Select(kv => new KeyValuePair<string, string?>(kv.Key, kv.Value)));
        });

    }

    public async Task InitializeAsync()
    {
        await _mongoDbContainer.StartAsync();
        await _pgsqlContainer.StartAsync();
    }

    public new async Task DisposeAsync()
    {
        await _mongoDbContainer.StopAsync();
        await _pgsqlContainer.StopAsync();
    }
}