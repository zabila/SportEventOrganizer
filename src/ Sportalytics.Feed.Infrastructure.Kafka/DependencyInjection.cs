using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sportalytics.Feed.Infrastructure.Kafka.Consumer;
using Sportalytics.Feed.Infrastructure.Kafka.Interfaces;

namespace Sportalytics.Feed.Infrastructure.Kafka;

public static class DependencyInjection
{
    public static IServiceCollection AddApiSportKafkaConsumer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        
        var consumerConfig = new ConsumerConfig()
        {
            BootstrapServers = configuration["Kafka:BootstrapServers"],
            GroupId = "api-sports",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };
        services.AddSingleton(consumerConfig);
        services.AddScoped<IKafkaConsumer<string, string>, KafkaApiSportsConsumer>();
        return services;
    }


}