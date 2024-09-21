using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sportalytics.Event.Application.interfaces;
using Sportalytics.Event.Infrastructure.Kafka.Producer;

namespace Sportalytics.Event.Infrastructure.Kafka;

public static class DependencyInjection
{
    public static IServiceCollection AddKafkaProducer(this IServiceCollection services, IConfiguration configuration)
    {
        var producerConfig = new ProducerConfig
        {
            BootstrapServers = configuration["Kafka:BootstrapServers"]
        };
        services.AddSingleton(producerConfig);
        services.AddScoped<IKafkaProducer<string, string>, KafkaProducer<string, string>>();

        return services;
    }
}