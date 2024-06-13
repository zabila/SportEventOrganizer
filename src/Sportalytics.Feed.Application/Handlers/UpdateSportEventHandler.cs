using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
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

        var sportEventQuery = repository.Query(es => es.Id == id);
        var sportEvent = await sportEventQuery.FirstOrDefaultAsync(cancellationToken);
        if (sportEvent == null)
        {
            throw new SportEventNotFoundException(id);
        }

        mapper.Map(sportEventDto, sportEvent);
        await repository.UpdateAsync(sportEvent, cancellationToken);
    }
}