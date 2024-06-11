namespace Sportalytics.Feed.Application.DTOs.Abstractions;


public abstract record SportEventDtoBase
{
    public string? Name { get; init; }
    public string? Location { get; init; }
    public DateTime Date { get; init; }
}
