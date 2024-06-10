using AutoMapper;
using Sportalytics.Application.Abstractions.Messaging;
using Sportalytics.Application.Commands;
using Sportalytics.Application.Interfaces;
using Sportalytics.Domain.Entities;
using Sportalytics.Domain.Shared;

namespace Sportalytics.Application.Handlers;

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