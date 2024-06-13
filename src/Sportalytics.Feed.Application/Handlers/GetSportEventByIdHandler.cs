using AutoMapper;
using MediatR;
using Sportalytics.Feed.Application.DTOs;
using Sportalytics.Feed.Application.Queries;
using Sportalytics.Feed.Domain.Entities;
using Sportalytics.Feed.Domain.Exceptions;
using Sportalytics.Feed.Persistence.MongoDB.Interfaces;

namespace Sportalytics.Feed.Application.Handlers;

internal sealed class GetSportEventByIdHandler(IRepository<SportEvent> repository, IMapper mapper) : IRequestHandler<GetSportEventByIdQuery, ResponseSportEventDto>
{
    public async Task<ResponseSportEventDto> Handle(GetSportEventByIdQuery request, CancellationToken cancellationToken)
    {
        var id = request.Id;
        var sportEventQuery = await repository.QueryAsync(es => es.Id == id, cancellationToken);
        var sportEvent = sportEventQuery.FirstOrDefault();
        if (sportEvent == null)
        {
            throw new SportEventNotFoundException(id);
        }

        var response = mapper.Map<ResponseSportEventDto>(sportEvent);
        return response;
    }
}