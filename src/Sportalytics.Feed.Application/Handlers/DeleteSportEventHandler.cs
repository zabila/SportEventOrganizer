using Sportalytics.Feed.Application.Abstractions.Messaging;
using Sportalytics.Feed.Application.Commands;
using Sportalytics.Feed.Application.Interfaces;
using Sportalytics.Feed.Domain.Shared;

namespace Sportalytics.Feed.Application.Handlers;

internal sealed class DeleteSportEventHandler(IRepositoryManager repository, IUnitOfWork unitOfWork) : ICommandHandler<DeleteSportEventCommand>
{

    public async Task<Result> Handle(DeleteSportEventCommand request, CancellationToken cancellationToken)
    {
        var id = request.Id;
        var sportEventRepository = repository.SportEvents;
        await sportEventRepository.DeleteByGuidAsync(id);
        await unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}