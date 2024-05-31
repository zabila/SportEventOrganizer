namespace Sportalytics.Domain.DTOs;

public record CreateSpotEventDto(
    string? Name,
    string? Location,
    DateTime Date);