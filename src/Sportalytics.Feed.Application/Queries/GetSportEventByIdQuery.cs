using MediatR;
using Sportalytics.Feed.Application.DTOs;

namespace Sportalytics.Feed.Application.Queries;

public sealed record GetSportEventByIdQuery(Guid SportId, CancellationToken CancellationToken) : IRequest<ResponseSportEventDto>
{
}