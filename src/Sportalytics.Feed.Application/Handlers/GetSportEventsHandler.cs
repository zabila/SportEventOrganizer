using AutoMapper;
using Sportalytics.Feed.Application.Abstractions.Messaging;
using Sportalytics.Feed.Application.DTOs;
using Sportalytics.Feed.Application.Interfaces;
using Sportalytics.Feed.Application.Queries;
using Sportalytics.Feed.Domain.Shared;

namespace Sportalytics.Feed.Application.Handlers;

internal sealed class GetSportEventsHandler(IRepositoryManager repository, IMapper mapper) : IQueryHandler<GetSportEventsQuery, List<ResponseSpotEventDto>>
{
    
    private static readonly Error SportEventsNotFoundError = new Error(
        "SportEventsNotFound", 
        "No sport events found");

    public async Task<Result<List<ResponseSpotEventDto>>> Handle(GetSportEventsQuery request, CancellationToken cancellationToken)
    {
        var sportEventRepository = repository.SportEvents;
        var sportEvents = await sportEventRepository.GetAllAsync();
        
        if (sportEvents.Count == 0)
            return Result.Failure<List<ResponseSpotEventDto>>(SportEventsNotFoundError);

        var response = mapper.Map<List<ResponseSpotEventDto>>(sportEvents);
        return Result.Success(response);
    }
}