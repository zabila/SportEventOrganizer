using System.ComponentModel.DataAnnotations.Schema;

namespace Sportalytics.Domain.Entities;

public class SportEvent
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Location { get; set; }
    public DateTime Date { get; set; }
}