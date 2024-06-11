namespace Sportalytics.Feed.Application.DTOs;

public sealed record UpdateSpotEventDto(
    string? Name,
    string? Location,
    DateTime Date);