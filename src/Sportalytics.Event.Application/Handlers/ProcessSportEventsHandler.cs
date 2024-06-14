using AutoMapper;
using MediatR;
using Sportalytics.Event.Application.Commands;
using Sportalytics.Event.Domain.Entities;
using Sportalytics.Event.Domain.Exceptions;

namespace Sportalytics.Event.Application.Handlers;

internal sealed class ProcessSportEventsHandler(IMapper mapper) : IRequestHandler<ProcessSportEventsCommand>
{

    public Task Handle(ProcessSportEventsCommand request, CancellationToken cancellationToken)
    {
        var response = request.ProcessApiSportsResponseDto.Response;
        if (response is null)
        {
            throw new ApiSportEventsNotFoundException();
        }

        foreach (var sportEvent in response.Select(mapper.Map<SportEvent>))
        {
            Console.WriteLine($"Sport event: {sportEvent.Name}, {sportEvent.Location}, {sportEvent.Date}");
        }

        return Task.CompletedTask;
    }
}