namespace Sportalytics.Event.Application.interfaces;

public interface IKafkaProducer<in TKey, in TValue>
{
    Task ProduceAsync(string topic, TKey key, TValue value, CancellationToken cancellationToken);
}