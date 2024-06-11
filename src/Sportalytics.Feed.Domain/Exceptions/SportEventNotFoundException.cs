namespace Sportalytics.Feed.Domain.Exceptions;

public class SportEventNotFoundException(Guid id) : NotFoundException($"Sport event with id {id} was not found.");