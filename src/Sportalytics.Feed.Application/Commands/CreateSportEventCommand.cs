using Sportalytics.Feed.Application.Abstractions.Messaging;
using Sportalytics.Feed.Application.DTOs;

namespace Sportalytics.Feed.Application.Commands;

public sealed record CreateSportEventCommand(CreateSpotEventDto CreateSpotEventDto, CancellationToken CancellationToken) : ICommand;