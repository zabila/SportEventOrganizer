using Sportalytics.Feed.Application.DTOs.Abstractions;

namespace Sportalytics.Feed.Application.DTOs;

public sealed record ConsumerResponseSportEventDto : SportEventDtoBase
{
    public Guid Id { get; init; }
};