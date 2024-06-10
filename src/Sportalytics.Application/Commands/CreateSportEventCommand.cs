using Sportalytics.Application.Abstractions.Messaging;
using Sportalytics.Application.DTOs;

namespace Sportalytics.Application.Commands;

public sealed record CreateSportEventCommand(CreateSpotEventDto CreateSpotEventDto, CancellationToken CancellationToken) : ICommand;