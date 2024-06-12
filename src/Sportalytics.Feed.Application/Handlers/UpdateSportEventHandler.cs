using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sportalytics.Feed.Application.Commands;
using Sportalytics.Feed.Application.Interfaces;
using Sportalytics.Feed.Domain.Exceptions;
using Sportalytics.Feed.Persistence.Filters;
using Sportalytics.Feed.Persistence.Interfaces;

namespace Sportalytics.Feed.Application.Handlers;

internal sealed class UpdateSportEventHandler(IRepositoryManager repository, IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<UpdateSportEventCommand>
{

    public async Task Handle(UpdateSportEventCommand request, CancellationToken cancellationToken)
    {
        var id = request.Id;
        var sportEventDto = request.UpdateSpotEventDto;

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

        mapper.Map(sportEventDto, sportEvent);
        await unitOfWork.SaveChangesAsync();
    }
}