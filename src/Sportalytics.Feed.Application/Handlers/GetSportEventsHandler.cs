using AutoMapper;
using Sportalytics.Feed.Application.Abstractions.Messaging;
using Sportalytics.Feed.Application.DTOs;
using Sportalytics.Feed.Application.Interfaces;
using Sportalytics.Feed.Application.Queries;
using Sportalytics.Feed.Domain.Shared;

namespace Sportalytics.Feed.Application.Handlers;

internal sealed class GetSportEventsHandler(IRepositoryManager repository, IMapper mapper) : IQueryHandler<GetSportEventsQuery, List<ResponseSportEventDto>>
{
    
    private static readonly Error SportEventsNotFoundError = new Error(
        "SportEventsNotFound", 
        "No sport events found");

    public async Task<Result<List<ResponseSportEventDto>>> Handle(GetSportEventsQuery request, CancellationToken cancellationToken)
    {
        var sportEventRepository = repository.SportEvents;
        var sportEvents = await sportEventRepository.GetAllAsync();
        
        if (sportEvents.Count == 0)
            return Result.Failure<List<ResponseSportEventDto>>(SportEventsNotFoundError);

        var response = mapper.Map<List<ResponseSportEventDto>>(sportEvents);
        return Result.Success(response);
    }
}