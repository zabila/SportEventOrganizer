using AutoMapper;
using MediatR;
using Sportalytics.Feed.Application.Commands;
using Sportalytics.Feed.Domain.Entities;
using Sportalytics.Feed.Domain.Exceptions;
using Sportalytics.Feed.Persistence.MongoDB.Interfaces;

namespace Sportalytics.Feed.Application.Handlers;

internal sealed class UpdateSportEventHandler(IRepository<SportEvent> repository, IMapper mapper) : IRequestHandler<UpdateSportEventCommand>
{

    public async Task Handle(UpdateSportEventCommand request, CancellationToken cancellationToken)
    {
        var id = request.Id;
        var sportEventDto = request.UpdateSpotEventDto;

        var sportEventQuery = await repository.QueryAsync(es => es.Id == id, cancellationToken);
        var sportEvent = sportEventQuery.FirstOrDefault();
        if (sportEvent == null)
        {
            throw new SportEventNotFoundException(id);
        }

        mapper.Map(sportEventDto, sportEvent);
        await repository.UpdateAsync(sportEvent, cancellationToken);
    }
}