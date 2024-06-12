using AutoMapper;
using MediatR;
using Sportalytics.Feed.Domain.Entities;
using Sportalytics.Feed.Application.Commands;
using Sportalytics.Feed.Persistence.Interfaces;

namespace Sportalytics.Feed.Application.Handlers;

internal sealed class CreateSportEventHandler(IRepositoryManager repositoryManager, IMapper mapper) : IRequestHandler<CreateSportEventCommand, Guid>
{
    public async Task<Guid> Handle(CreateSportEventCommand request, CancellationToken cancellationToken)
    {
        var sportEvent = mapper.Map<SportEvent>(request.CreateSportEventDto);

        var sportEventRepository = repositoryManager.SportEvents;
        await sportEventRepository.AddAsync(sportEvent, cancellationToken);
        await repositoryManager.SaveChangesAsync();
        return sportEvent.Id;
    }
}