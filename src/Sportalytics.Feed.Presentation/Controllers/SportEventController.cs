using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sportalytics.Feed.Application.Commands;
using Sportalytics.Feed.Application.DTOs;
using Sportalytics.Feed.Application.Queries;

namespace Sportalytics.Feed.Presentation.Controllers;

[Route("api/sport-events")]
[ApiController]
public sealed class SportEventController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateSportEvent([FromBody] CreateSportEventDto createSportEventDto, CancellationToken cancellationToken)
    {
        var command = new CreateSportEventCommand(createSportEventDto, cancellationToken);
        var id = await sender.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetSportEventById), new
        {
            id = id
        }, createSportEventDto);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetSportEventById(Guid id, CancellationToken cancellationToken)
    {
        var command = new GetSportEventByIdQuery(id, cancellationToken);
        var result = await sender.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetSportEvents(CancellationToken cancellationToken)
    {
        var command = new GetSportEventsQuery(cancellationToken);
        var result = await sender.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateSportEvent(Guid id, [FromBody] UpdateSpotEventDto updateSportEventDto, CancellationToken cancellationToken)
    {
        var command = new UpdateSportEventCommand(id, updateSportEventDto, cancellationToken);
        await sender.Send(command, cancellationToken);
        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteSportEvent(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteSportEventCommand(id);
        await sender.Send(command, cancellationToken);
        return Ok();
    }
}