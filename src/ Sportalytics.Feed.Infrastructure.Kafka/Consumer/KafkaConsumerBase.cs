using Confluent.Kafka;
using MediatR;
using Sportalytics.Feed.Infrastructure.Kafka.Interfaces;

namespace Sportalytics.Feed.Infrastructure.Kafka.Consumer;

public abstract class KafkaConsumerBase<TKey, TValue>(ConsumerConfig config) : IKafkaConsumer<TKey, TValue>, IDisposable
{
    protected readonly IConsumer<TKey, TValue> Consumer = new ConsumerBuilder<TKey, TValue>(config).Build();

    public abstract Task ConsumeAsync(string topic, CancellationToken cancellationToken);

    public void Dispose()
    {
        Consumer.Close();
        Consumer.Dispose();
    }
}