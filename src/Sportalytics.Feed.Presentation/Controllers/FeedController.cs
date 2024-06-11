using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sportalytics.Feed.Application.Commands;
using Sportalytics.Feed.Application.DTOs;
using Sportalytics.Feed.Application.Queries;

namespace Sportalytics.Feed.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class FeedController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateSportEvent([FromBody] CreateSpotEventDto createSpotEventDto, CancellationToken cancellationToken)
    {
        var command = new CreateSportEventCommand(createSpotEventDto, cancellationToken);
        var result = await sender.Send(command, cancellationToken);
        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetSportEventById(Guid id, CancellationToken cancellationToken)
    {
        var command = new GetSportEventByIdQuery(id, cancellationToken);
        var result = await sender.Send(command, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    }
}