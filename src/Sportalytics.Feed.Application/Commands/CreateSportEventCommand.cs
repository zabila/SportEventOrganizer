using MediatR;
using Sportalytics.Feed.Application.DTOs;

namespace Sportalytics.Feed.Application.Commands;

public sealed record CreateSportEventCommand(CreateSportEventDto CreateSportEventDto, CancellationToken CancellationToken) : IRequest<Guid>;