using Sportalytics.Application.Abstractions.Messaging;
using Sportalytics.Application.DTOs;

namespace Sportalytics.Application.Queries;

public sealed record GetSportEventByIdQuery(Guid SportId, CancellationToken CancellationToken) : IQuery<ResponseSpotEventDto>
{
}