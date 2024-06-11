using Sportalytics.Feed.Application.DTOs.Abstractions;

namespace Sportalytics.Feed.Application.DTOs;

public sealed record ResponseSportEventDto : SportEventDtoBase
{
    public Guid Id { get; init; }
};