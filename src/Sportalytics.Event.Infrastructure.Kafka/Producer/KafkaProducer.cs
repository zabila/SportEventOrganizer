using Confluent.Kafka;
using Sportalytics.Event.Application.interfaces;

namespace Sportalytics.Event.Infrastructure.Kafka.Producer;

public class KafkaProducer<TKey, TValue>(ProducerConfig config) : IKafkaProducer<TKey, TValue>, IDisposable
{
    private readonly IProducer<TKey, TValue> _producer = new ProducerBuilder<TKey, TValue>(config).Build();

    public async Task ProduceAsync(string topic, TKey key, TValue value, CancellationToken cancellationToken)
    {
        try
        {
            var message = new Message<TKey, TValue>
            {
                Key = key,
                Value = value
            };

            var result = await _producer.ProduceAsync(topic, message, cancellationToken);
            Console.WriteLine($"Delivered '{result.Value}' to '{result.TopicPartitionOffset}'");
        }
        catch (KafkaException exception)
        {
            Console.WriteLine(exception);
            throw;
        }
    }

    public void Dispose()
    {
        _producer.Flush();
        _producer.Dispose();
    }
}