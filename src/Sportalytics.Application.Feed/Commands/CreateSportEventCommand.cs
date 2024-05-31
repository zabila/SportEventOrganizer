using Sportalytics.Application.Abstractions.Messaging;
using Sportalytics.Domain.DTOs;

namespace Sportalytics.Application.Feed.Commands;

public sealed record CreateSportEventCommand(CreateSpotEventDto CreateSpotEventDto, CancellationToken CancellationToken) : ICommand;