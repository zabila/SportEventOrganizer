using AutoMapper;
using Sportalytics.Feed.Domain.Entities;
using Sportalytics.Feed.Domain.Shared;
using Sportalytics.Feed.Application.Abstractions.Messaging;
using Sportalytics.Feed.Application.Commands;
using Sportalytics.Feed.Application.Interfaces;

namespace Sportalytics.Feed.Application.Handlers;

internal sealed class CreateSportEventHandler(IRepositoryManager repositoryManager, IUnitOfWork unitOfWork, IMapper mapper) : ICommandHandler<CreateSportEventCommand>
{
    public async Task<Result> Handle(CreateSportEventCommand request, CancellationToken cancellationToken)
    {
        var sportEvent = mapper.Map<SportEvent>(request.CreateSpotEventDto);

        var sportEventRepository = repositoryManager.SportEvents;
        await sportEventRepository.AddAsync(sportEvent, cancellationToken);
        await unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}