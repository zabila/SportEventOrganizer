using MediatR;
using Sportalytics.Event.Application.DTOs;

namespace Sportalytics.Event.Application.Commands;

public sealed record ProcessSportEventsCommand(ProcessApiSportsResponseDto ProcessApiSportsResponseDto, CancellationToken CancellationToken) : IRequest;