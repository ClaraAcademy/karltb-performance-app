using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PerformanceApp.Server.Services;

namespace PerformanceApp.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class SvgController(ISvgService service) : ControllerBase
{
    private readonly ISvgService _service = service;


    /// <summary>
    /// Gets a cumulative-performance graph in SVG format for a specific portfolio.
    /// </summary>
    /// <returns>A string containing the SVG representation of the graph.</returns>
    /// <param name="portfolioId">The ID of the portfolio for which to create the graph.</param>
    /// <param name="width">The width of the SVG graph.</param>
    /// <param name="height">The height of the SVG graph.</param>
    /// <remarks>
    /// Sample request:
    /// <code>
    /// GET /api/svg?portfolioId=1&amp;width=800&amp;height=600
    /// Authorization: Bearer {token}
    /// </code>
    /// </remarks>
    /// <response code="200">Returns the SVG graph as a string</response>
    /// <response code="404">If no graph could be generated for the specified portfolio</response>
    [HttpGet]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<string>> GetCumulativePerformanceGraph(
        [FromQuery] int portfolioId,
        [FromQuery] int? width = null,
        [FromQuery] int? height = null
    )
    {
        var svg = await _service.GetLineChart(portfolioId, width, height);

        if (svg == string.Empty)
        {
            return NotFound();
        }

        return Content(svg, "image/svg+xml");
    }
}