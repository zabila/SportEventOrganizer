using MediatR;
using Sportalytics.Feed.Application.Commands;
using Sportalytics.Feed.Domain.Entities;
using Sportalytics.Feed.Domain.Exceptions;
using Sportalytics.Feed.Persistence.MongoDB.Interfaces;

namespace Sportalytics.Feed.Application.Handlers;

internal sealed class DeleteSportEventHandler(IRepository<SportEvent> repository) : IRequestHandler<DeleteSportEventCommand>
{

    public async Task Handle(DeleteSportEventCommand request, CancellationToken cancellationToken)
    {
        var id = request.Id;
        var sportEventQuery = await repository.QueryAsync(es => es.Id == id, cancellationToken);
        var sportEvent = sportEventQuery.FirstOrDefault();
        if (sportEvent == null)
        {
            throw new SportEventNotFoundException(id);
        }
        await repository.DeleteAsync(sportEvent, cancellationToken);
    }
}