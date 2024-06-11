namespace Sportalytics.Feed.Application.DTOs;

public record CreateSpotEventDto(
    string? Name,
    string? Location,
    DateTime Date);