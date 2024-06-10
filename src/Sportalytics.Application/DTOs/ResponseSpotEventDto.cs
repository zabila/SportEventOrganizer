namespace Sportalytics.Application.DTOs;

public sealed record ResponseSpotEventDto(
    Guid Id,
    string? Name,
    string? Location,
    DateTime Date);