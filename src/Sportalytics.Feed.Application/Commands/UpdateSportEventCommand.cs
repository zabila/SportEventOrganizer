using MediatR;
using Sportalytics.Feed.Application.DTOs;

namespace Sportalytics.Feed.Application.Commands;

public sealed record UpdateSportEventCommand(Guid Id, UpdateSpotEventDto UpdateSpotEventDto, CancellationToken CancellationToken) : IRequest;