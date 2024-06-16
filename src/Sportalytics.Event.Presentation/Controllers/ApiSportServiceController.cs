using Microsoft.AspNetCore.Mvc;
using Sportalytics.Event.Infrastructure.Interfaces;

namespace Sportalytics.Event.Presentation.Controllers;

[Route("api-sport-service")]
[ApiController]
public class ApiSportServiceController(IBackgroundApiSportService backgroundApiSportService) : ControllerBase
{
    [HttpGet("start")]
    public IActionResult Start()
    {
        backgroundApiSportService.Start();
        return Ok();
    }

    [HttpGet("stop")]
    public IActionResult Stop()
    {
        backgroundApiSportService.Stop();
        return Ok();
    }
}