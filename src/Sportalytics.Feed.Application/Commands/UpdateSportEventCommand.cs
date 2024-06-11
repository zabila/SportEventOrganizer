using Sportalytics.Feed.Application.Abstractions.Messaging;
using Sportalytics.Feed.Application.DTOs;

namespace Sportalytics.Feed.Application.Commands;

public sealed record UpdateSportEventCommand(Guid Guid, UpdateSpotEventDto UpdateSpotEventDto, CancellationToken CancellationToken) : ICommand;