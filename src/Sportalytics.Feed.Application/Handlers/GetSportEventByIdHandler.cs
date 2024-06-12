using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sportalytics.Feed.Application.DTOs;
using Sportalytics.Feed.Application.Queries;
using Sportalytics.Feed.Domain.Exceptions;
using Sportalytics.Feed.Persistence.Filters;
using Sportalytics.Feed.Persistence.Interfaces;

namespace Sportalytics.Feed.Application.Handlers;

internal sealed class GetSportEventByIdHandler(IRepositoryManager repositoryManager, IMapper mapper) : IRequestHandler<GetSportEventByIdQuery, ResponseSportEventDto>
{
    public async Task<ResponseSportEventDto> Handle(GetSportEventByIdQuery request, CancellationToken cancellationToken)
    {
        var id = request.SportId;
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
        if (sportEvent == null)
        {
            throw new SportEventNotFoundException(id);
        }

        var response = mapper.Map<ResponseSportEventDto>(sportEvent);
        return response;
    }
}