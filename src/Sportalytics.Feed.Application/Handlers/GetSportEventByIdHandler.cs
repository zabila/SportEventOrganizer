using AutoMapper;
using Sportalytics.Feed.Domain.Shared;
using Sportalytics.Feed.Application.Abstractions.Messaging;
using Sportalytics.Feed.Application.DTOs;
using Sportalytics.Feed.Application.Interfaces;
using Sportalytics.Feed.Application.Queries;

namespace Sportalytics.Feed.Application.Handlers;

internal sealed class GetSportEventByIdHandler(IRepositoryManager repositoryManager, IMapper mapper) : IQueryHandler<GetSportEventByIdQuery, ResponseSpotEventDto>
{

    public async Task<Result<ResponseSpotEventDto>> Handle(GetSportEventByIdQuery request, CancellationToken cancellationToken)
    {
        var id = request.SportId;
        var sportEventRepository = repositoryManager.SportEvents;
        var sportEvent = await sportEventRepository.GetByIdAsync(id);
        if (sportEvent is null)
        {
            return Result.Failure<ResponseSpotEventDto>(new Error(
                "SportEventNotFound",
                $"Sport event with id {id} not found"
            ));
        }

        var response = mapper.Map<ResponseSpotEventDto>(sportEvent);
        return Result.Success(response);
    }

}