using AutoMapper;
using MediatR;
using Sportalytics.Event.Application.Commands;
using Sportalytics.Event.Domain.Entities;
using Sportalytics.Event.Domain.Exceptions;
using Sportalytics.Event.Domain.Extensions;

namespace Sportalytics.Event.Application.Handlers;

internal sealed class ProcessSportEventsHandler(IMapper mapper) : IRequestHandler<ProcessSportEventsCommand>
{

    public Task Handle(ProcessSportEventsCommand request, CancellationToken cancellationToken)
    {
        var response = request.ProcessApiSportsResponseDto.Response.EnsureExists();
        foreach (var sportEvent in response.Select(mapper.Map<SportEvent>))
        {
            Console.WriteLine($"Sport event: {sportEvent.Name}, {sportEvent.Location}, {sportEvent.Date}");
        }

        return Task.CompletedTask;
    }
}