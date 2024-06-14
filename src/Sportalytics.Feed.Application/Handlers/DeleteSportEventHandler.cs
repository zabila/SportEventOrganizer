using MediatR;
using Microsoft.EntityFrameworkCore;
using Sportalytics.Feed.Application.Commands;
using Sportalytics.Feed.Application.Extensions;
using Sportalytics.Feed.Domain.Entities;
using Sportalytics.Feed.Persistence.MongoDB.Interfaces;

namespace Sportalytics.Feed.Application.Handlers;

internal sealed class DeleteSportEventHandler(IRepository<SportEvent> repository) : IRequestHandler<DeleteSportEventCommand>
{

    public async Task Handle(DeleteSportEventCommand request, CancellationToken cancellationToken)
    {
        var id = request.Id;
        var sportEventQuery = repository.Query(es => es.Id == id);
        var sportEvent = await sportEventQuery.FirstOrDefaultAsync(cancellationToken).EnsureFound();
        await repository.DeleteAsync(sportEvent!, cancellationToken);
    }
}