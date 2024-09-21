using MediatR;
namespace Sportalytics.Feed.Application.Commands;

public sealed record DeleteSportEventCommand(Guid Id) : IRequest;