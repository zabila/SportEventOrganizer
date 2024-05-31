namespace Sportalytics.Domain.DTOs;

public sealed record ResponseSpotEventDto(
    Guid Id,
    string? Name,
    string? Location,
    DateTime Date);