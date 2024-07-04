using AutoMapper;
using MediatR;
using Newtonsoft.Json;
using Sportalytics.Event.Application.Commands;
using Sportalytics.Event.Application.interfaces;
using Sportalytics.Event.Domain.Entities;
using Sportalytics.Event.Domain.Extensions;

namespace Sportalytics.Event.Application.Handlers;

internal sealed class ProcessSportEventsHandler(IMapper mapper, IKafkaProducer<string, string> producer) : IRequestHandler<ProcessSportEventsCommand>
{
    private const string Topic = "api-sports";

    public async Task Handle(ProcessSportEventsCommand request, CancellationToken cancellationToken)
    {
        var response = request.ProcessApiSportsResponseDto.Response.EnsureExists();
        foreach (var sportEvent in response.Select(mapper.Map<SportEvent>))
        {
            Console.WriteLine($"Sport event: {sportEvent.Name}, {sportEvent.Location}, {sportEvent.Date}");
            await producer.ProduceAsync(Topic, sportEvent.Id.ToString(), JsonConvert.SerializeObject(sportEvent), cancellationToken);
        }
    }
}