using Microsoft.AspNetCore.Mvc;
using Sportalytics.Event.Infrastructure.Interfaces;

namespace Sportalytics.Event.Presentation.Controllers;

[Route("api-sport-service")]
[ApiController]
public class ApiSportServiceController(IScopedBackgroundApiSportService scopedBackgroundApiSportService) : ControllerBase
{
    [HttpGet("start")]
    public IActionResult Start()
    {
        scopedBackgroundApiSportService.Start();
        return Ok();
    }

    [HttpGet("stop")]
    public IActionResult Stop()
    {
        scopedBackgroundApiSportService.Stop();
        return Ok();
    }
}