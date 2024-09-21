using Sportalytics.Event.Domain.Interfaces;

namespace Sportalytics.Event.Domain.Entities;

public class SportEvent : IEntity
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Location { get; set; }
    public DateTime Date { get; set; }
}