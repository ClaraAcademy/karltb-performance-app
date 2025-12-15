using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PerformanceApp.Data.Dtos;
using PerformanceApp.Server.Services;

namespace PerformanceApp.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PerformanceController(IPerformanceService service) : MyControllerBase
{
    private readonly IPerformanceService Service = service;

    // GET: api/performance?portfolioId={portfolioId}
    /// <summary>
    /// Gets a list of Portfolio-Benchmark key-figure values for a specific portfolio.
    /// </summary>
    /// <param name="portfolioId">The ID of the portfolio to retrieve key-figure values for.</param>
    /// <returns>A list of PortfolioBenchmarkKeyFigureDTO objects containing the key-figure values for the specified portfolio.</returns>
    /// <remarks>
    /// Sample request:
    /// <code>
    /// GET /api/performance?portfolioId=1
    /// Authorization: Bearer {token}
    /// </code>
    /// </remarks>
    /// <response code="200">Returns the list of Portfolio-Benchmark key-figure values</response>
    /// <response code="401">If the user is not authenticated</response>
    /// <response code="404">If no key-figure values are found for the specified portfolio</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PortfolioBenchmarkKeyFigureDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<PortfolioBenchmarkKeyFigureDTO>>> GetKeyFigures([FromQuery] int portfolioId)
    {
        if (!UserIsAuthenticated())
        {
            return UnauthorizedResponse();
        }

        var dtos = await Service.GetPortfolioBenchmarkKeyFigureValues(portfolioId);

        return CheckReturn(dtos);
    }
}