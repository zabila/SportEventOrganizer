using Sportalytics.Application.Abstractions.Messaging;
using Sportalytics.Domain.DTOs;

namespace Sportalytics.Application.Feed.Queries;

public sealed record GetSportEventByIdQuery(Guid SportId, CancellationToken CancellationToken) : IQuery<ResponseSpotEventDto>
{
}