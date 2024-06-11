using AutoMapper;
using MediatR;
using Sportalytics.Feed.Application.Commands;
using Sportalytics.Feed.Application.Interfaces;

namespace Sportalytics.Feed.Application.Handlers;

internal sealed class UpdateSportEventHandler(IRepositoryManager repository, IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<UpdateSportEventCommand>
{

    public async Task Handle(UpdateSportEventCommand request, CancellationToken cancellationToken)
    {
        var guid = request.Guid;
        var sportEventDto = request.UpdateSpotEventDto;

        var sportEventRepository = repository.SportEvents;
        var sportEventEntity = await sportEventRepository.GetByIdAsync(guid);
        mapper.Map(sportEventDto, sportEventEntity);
        await unitOfWork.SaveChangesAsync();
    }
}