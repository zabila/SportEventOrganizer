using AutoMapper;
using MediatR;
using Sportalytics.Feed.Domain.Entities;
using Sportalytics.Feed.Application.Commands;
using Sportalytics.Feed.Persistence.MongoDB.Interfaces;

namespace Sportalytics.Feed.Application.Handlers;

internal sealed class CreateSportEventHandler(IRepository<SportEvent> repository, IMapper mapper) : IRequestHandler<CreateSportEventCommand, Guid>
{
    public async Task<Guid> Handle(CreateSportEventCommand request, CancellationToken cancellationToken)
    {
        var sportEvent = mapper.Map<SportEvent>(request.CreateSportEventDto);
        await repository.AddAsync(sportEvent, cancellationToken);
        return sportEvent.Id;
    }
}