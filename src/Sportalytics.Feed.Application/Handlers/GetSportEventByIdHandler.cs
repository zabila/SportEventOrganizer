using AutoMapper;
using Sportalytics.Feed.Domain.Shared;
using Sportalytics.Feed.Application.Abstractions.Messaging;
using Sportalytics.Feed.Application.DTOs;
using Sportalytics.Feed.Application.Interfaces;
using Sportalytics.Feed.Application.Queries;

namespace Sportalytics.Feed.Application.Handlers;

internal sealed class GetSportEventByIdHandler(IRepositoryManager repositoryManager, IMapper mapper) : IQueryHandler<GetSportEventByIdQuery, ResponseSportEventDto>
{
    private readonly Result<ResponseSportEventDto> _sportEventNotFound = Result.Failure<ResponseSportEventDto>(new Error(
        "SportEventNotFound",
        "Sport event not found"
    ));

    public async Task<Result<ResponseSportEventDto>> Handle(GetSportEventByIdQuery request, CancellationToken cancellationToken)
    {
        var id = request.SportId;
        var sportEventRepository = repositoryManager.SportEvents;
        var sportEvent = await sportEventRepository.GetByIdAsync(id);
        if (sportEvent is null)
        {
            return _sportEventNotFound;
        }

        var response = mapper.Map<ResponseSportEventDto>(sportEvent);
        return Result.Success(response);
    }

}