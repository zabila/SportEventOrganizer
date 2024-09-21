using Sportalytics.Event.Domain.Entities.ApiSports;

namespace Sportalytics.Event.Application.DTOs;

public record ProcessApiSportsResponseDto(List<Response>? Response);