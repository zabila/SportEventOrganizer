using Sportalytics.Feed.Application.Abstractions.Messaging;
using Sportalytics.Feed.Application.DTOs;

namespace Sportalytics.Feed.Application.Queries;

public sealed record GetSportEventsQuery(CancellationToken CancellationToken) : IQuery<List<ResponseSpotEventDto>>
{
}