namespace Sportalytics.Feed.Infrastructure.Kafka.Interfaces;

public interface IKafkaConsumer<TKey, TValue>
{
    Task ConsumeAsync(string topic, CancellationToken cancellationToken);
}