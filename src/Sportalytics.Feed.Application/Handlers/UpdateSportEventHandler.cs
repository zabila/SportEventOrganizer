using AutoMapper;
using Sportalytics.Feed.Application.Abstractions.Messaging;
using Sportalytics.Feed.Application.Commands;
using Sportalytics.Feed.Application.Interfaces;
using Sportalytics.Feed.Domain.Entities;
using Sportalytics.Feed.Domain.Shared;

namespace Sportalytics.Feed.Application.Handlers;

internal sealed class UpdateSportEventHandler(IRepositoryManager repository, IUnitOfWork unitOfWork ,IMapper  mapper) : ICommandHandler<UpdateSportEventCommand>
{

    public async Task<Result> Handle(UpdateSportEventCommand request, CancellationToken cancellationToken)
    {
        var guid = request.Guid;
        var sportEventDto = request.UpdateSpotEventDto;
        var sportEvent = mapper.Map<SportEvent>(sportEventDto);
        
        var sportEventRepository = repository.SportEvents;
        await sportEventRepository.UpdateAsync(guid, sportEvent);
        await unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}