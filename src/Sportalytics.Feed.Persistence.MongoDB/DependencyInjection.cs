using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using Sportalytics.Feed.Domain.Entities;
using Sportalytics.Feed.Domain.Extensions;
using Sportalytics.Feed.Domain.Interfaces;
using Sportalytics.Feed.Persistence.MongoDB.Core;
using Sportalytics.Feed.Persistence.MongoDB.Interfaces;
using Sportalytics.Feed.Persistence.MongoDB.Settings;

namespace Sportalytics.Feed.Persistence.MongoDB;

public static class DependencyInjection
{
    public static IServiceCollection AddMongo(this IServiceCollection services, IConfiguration configuration)
    {
        if (BsonSerializer.LookupSerializer(typeof(Guid)) == null) BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
        if (BsonSerializer.LookupSerializer(typeof(DateTimeOffset)) == null) BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));

        var mongoDbSettings = new MongoDbSettings();
        configuration.GetSection(nameof(MongoDbSettings)).Bind(mongoDbSettings);

        ArgumentNullException.ThrowIfNull(mongoDbSettings.Host);
        ArgumentNullException.ThrowIfNull(mongoDbSettings.Port);
        ArgumentNullException.ThrowIfNull(mongoDbSettings.DatabaseName);

        services.AddSingleton(a =>
        {
            var settings = new MongoClientSettings
            {
                Scheme = ConnectionStringScheme.MongoDB,
                Server = new MongoServerAddress(mongoDbSettings.Host, mongoDbSettings.Port),
                Credential = MongoCredential.CreateCredential("admin", mongoDbSettings.Username, mongoDbSettings.Password)
            };
            var mongoClient = new MongoClient(settings);
            return mongoClient.GetDatabase(mongoDbSettings.DatabaseName);
        });

        AddRepositories(services, mongoDbSettings);
        return services;
    }

    private static void AddRepositories(IServiceCollection services, MongoDbSettings settings)
    {
        services.AddMongoRepository<SportEvent>(settings.SportEventCollectionName);
    }

    private static IServiceCollection AddMongoRepository<T>(this IServiceCollection services, string? collectionName) where T : IEntity
    {
        services.AddSingleton<IRepository<T>>(a => new MongoRepository<T>(a.GetRequiredService<IMongoDatabase>(), collectionName.EnsureExists()));
        return services;
    }
}