﻿using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sportalytics.Feed.Application.DTOs;
using Sportalytics.Feed.Application.Queries;
using Sportalytics.Feed.Persistence.PostgreSQL.Filters;
using Sportalytics.Feed.Persistence.PostgreSQL.Interfaces;

namespace Sportalytics.Feed.Application.Handlers;

internal sealed class GetSportEventsHandler(IRepositoryManager repositoryManager, IMapper mapper) : IRequestHandler<GetSportEventsQuery, List<ResponseSportEventDto>>
{
    public async Task<List<ResponseSportEventDto>> Handle(GetSportEventsQuery request, CancellationToken cancellationToken)
    {
        var sportEventRepository = repositoryManager.SportEvents;
        var sportEventQuery = sportEventRepository.Query(new SportEventFilter());
        var sportEvents = await sportEventQuery.ToListAsync(cancellationToken);
        var response = mapper.Map<List<ResponseSportEventDto>>(sportEvents);
        return response;
    }
}