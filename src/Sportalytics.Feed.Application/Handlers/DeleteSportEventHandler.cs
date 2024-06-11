using MediatR;
using Sportalytics.Feed.Application.Commands;
using Sportalytics.Feed.Application.Interfaces;

namespace Sportalytics.Feed.Application.Handlers;

internal sealed class DeleteSportEventHandler(IRepositoryManager repository, IUnitOfWork unitOfWork) : IRequestHandler<DeleteSportEventCommand>
{

    public async Task Handle(DeleteSportEventCommand request, CancellationToken cancellationToken)
    {
        var id = request.Id;
        var sportEventRepository = repository.SportEvents;
        await sportEventRepository.DeleteByGuidAsync(id);
        await unitOfWork.SaveChangesAsync();
    }
}