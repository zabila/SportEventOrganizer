using AutoMapper;
using MediatR;
using Sportalytics.Feed.Application.DTOs;
using Sportalytics.Feed.Application.Interfaces;
using Sportalytics.Feed.Application.Queries;
using Sportalytics.Feed.Domain.Exceptions;

namespace Sportalytics.Feed.Application.Handlers;

internal sealed class GetSportEventByIdHandler(IRepositoryManager repositoryManager, IMapper mapper) : IRequestHandler<GetSportEventByIdQuery, ResponseSportEventDto>
{
    public async Task<ResponseSportEventDto> Handle(GetSportEventByIdQuery request, CancellationToken cancellationToken)
    {
        var id = request.SportId;
        var sportEventRepository = repositoryManager.SportEvents;
        var sportEvent = await sportEventRepository.GetByIdAsync(id);
        if (sportEvent is null)
        {
            throw new SportEventNotFoundException(id);
        }

        var response = mapper.Map<ResponseSportEventDto>(sportEvent);
        return response;
    }
}