using MediatR;
using Microsoft.EntityFrameworkCore;
using Sportalytics.Feed.Application.Commands;
using Sportalytics.Feed.Application.Interfaces;
using Sportalytics.Feed.Domain.Exceptions;
using Sportalytics.Feed.Persistence.Filters;
using Sportalytics.Feed.Persistence.Interfaces;

namespace Sportalytics.Feed.Application.Handlers;

internal sealed class DeleteSportEventHandler(IRepositoryManager repository, IUnitOfWork unitOfWork) : IRequestHandler<DeleteSportEventCommand>
{

    public async Task Handle(DeleteSportEventCommand request, CancellationToken cancellationToken)
    {
        var id = request.Id;
        var sportEventRepository = repository.SportEvents;

        var sportEventFilter = new SportEventFilter
        {
            Ids = new[]
            {
                id
            }
        };

        var sportEventQuery = sportEventRepository.Query(sportEventFilter);
        var sportEvent = await sportEventQuery.FirstOrDefaultAsync(cancellationToken);
        if (sportEvent == null)
        {
            throw new SportEventNotFoundException(id);
        }

        sportEventRepository.Delete(sportEvent);
        await unitOfWork.SaveChangesAsync();
    }
}