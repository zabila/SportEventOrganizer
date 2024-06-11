using Sportalytics.Feed.Application.Abstractions.Messaging;

namespace Sportalytics.Feed.Application.Commands;

public sealed record DeleteSportEventCommand(Guid Id) : ICommand;