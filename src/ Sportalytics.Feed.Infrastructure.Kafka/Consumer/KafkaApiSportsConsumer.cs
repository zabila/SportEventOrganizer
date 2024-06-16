using AutoMapper;
using Confluent.Kafka;
using MediatR;
using Newtonsoft.Json;
using Sportalytics.Feed.Application.Commands;
using Sportalytics.Feed.Application.DTOs;
using Sportalytics.Feed.Domain.Extensions;

namespace Sportalytics.Feed.Infrastructure.Kafka.Consumer;

public class KafkaApiSportsConsumer(ConsumerConfig config, ISender sender, IMapper mapper) : KafkaConsumerBase<string, string>(config)
{
    public override async Task ConsumeAsync(string topic, CancellationToken cancellationToken)
    {
        Consumer.Subscribe(topic);
        while (true)
        {
            var consumeResult = Consumer.Consume(cancellationToken);
            Console.WriteLine($"Consumed id {consumeResult.Message.Key}, message '{consumeResult.Message.Value}' at: '{consumeResult.TopicPartitionOffset}'.");
            
            var consumerResponseSportEventDto = JsonConvert.DeserializeObject<ConsumerResponseSportEventDto>(consumeResult.Message.Value).EnsureExists();
            var command = new CreateSportEventCommand(mapper.Map<CreateSportEventDto>(consumerResponseSportEventDto), cancellationToken);
            await sender.Send(command, cancellationToken);
            
            Consumer.Commit(consumeResult);
        }
    }
}