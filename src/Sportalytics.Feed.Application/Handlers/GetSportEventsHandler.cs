using AutoMapper;
using MediatR;
using Sportalytics.Feed.Application.DTOs;
using Sportalytics.Feed.Application.Interfaces;
using Sportalytics.Feed.Application.Queries;

namespace Sportalytics.Feed.Application.Handlers;

internal sealed class GetSportEventsHandler(IRepositoryManager repository, IMapper mapper) : IRequestHandler<GetSportEventsQuery, List<ResponseSportEventDto>>
{
    public async Task<List<ResponseSportEventDto>> Handle(GetSportEventsQuery request, CancellationToken cancellationToken)
    {
        var sportEventRepository = repository.SportEvents;
        var sportEvents = await sportEventRepository.GetAllAsync();
        var response = mapper.Map<List<ResponseSportEventDto>>(sportEvents);
        return response;
    }
}