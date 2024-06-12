using MediatR;
using Microsoft.EntityFrameworkCore;
using Sportalytics.Feed.Application.Commands;
using Sportalytics.Feed.Domain.Exceptions;
using Sportalytics.Feed.Persistence.PostgreSQL.Filters;
using Sportalytics.Feed.Persistence.PostgreSQL.Interfaces;

namespace Sportalytics.Feed.Application.Handlers;

internal sealed class DeleteSportEventHandler(IRepositoryManager repositoryManager) : IRequestHandler<DeleteSportEventCommand>
{

    public async Task Handle(DeleteSportEventCommand request, CancellationToken cancellationToken)
    {
        var id = request.Id;
        var sportEventRepository = repositoryManager.SportEvents;

        var sportEventFilter = new SportEventFilter
        {
            Ids = new[]
            {
                id
            }
        };

        var sportEventQuery = sportEventRepository.Query(sportEventFilter);
        var sportEvent = await sportEventQuery.FirstOrDefaultAsync(cancellationToken);
        if (sportEvent is null)
        {
            throw new SportEventNotFoundException(id);
        }

        sportEventRepository.Delete(sportEvent);
        await repositoryManager.SaveChangesAsync();
    }
}