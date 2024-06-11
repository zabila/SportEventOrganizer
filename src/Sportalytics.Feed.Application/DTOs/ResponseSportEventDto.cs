namespace Sportalytics.Feed.Application.DTOs;

public sealed record ResponseSportEventDto(
    Guid Id,
    string? Name,
    string? Location,
    DateTime Date);