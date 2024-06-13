using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sportalytics.Feed.Application.DTOs;
using Sportalytics.Feed.Application.Queries;
using Sportalytics.Feed.Domain.Entities;
using Sportalytics.Feed.Persistence.MongoDB.Interfaces;

namespace Sportalytics.Feed.Application.Handlers;

internal sealed class GetSportEventsHandler(IRepository<SportEvent> repository, IMapper mapper) : IRequestHandler<GetSportEventsQuery, List<ResponseSportEventDto>>
{
    public async Task<List<ResponseSportEventDto>> Handle(GetSportEventsQuery request, CancellationToken cancellationToken)
    {
        var sportEventQuery = await repository.QueryAsync(_ => true, cancellationToken);
        var sportEvents = sportEventQuery.ToList();
        var response = mapper.Map<List<ResponseSportEventDto>>(sportEvents);
        return response;
    }
}