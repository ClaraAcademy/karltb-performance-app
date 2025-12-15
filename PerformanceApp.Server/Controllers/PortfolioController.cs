using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PerformanceApp.Data.Dtos;
using PerformanceApp.Server.Services;

namespace PerformanceApp.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PortfolioController(IPortfolioService service) : MyControllerBase
{
    private readonly IPortfolioService _service = service;

    private string? GetUserId()
    {
        return User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }

    // GET: api/Portfolio
    /// <summary>
    /// Gets a list of portfolios for the authenticated user.
    /// </summary>
    /// <returns>A list of PortfolioDTO objects containing the portfolios for the authenticated user.</returns>
    /// <remarks>
    /// Sample request:
    /// <code>
    /// GET /api/portfolio
    /// Authorization: Bearer {token}
    /// </code>
    /// </remarks>
    /// <response code="200">Returns the portfolio list</response>
    /// <response code="404">If no portfolios are found for the user</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PortfolioDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<PortfolioDTO>>> GetPortfolios()
    {
        var userID = GetUserId();
        if (userID == null)
        {
            return Ok(new List<PortfolioDTO>());
        }

        var dtos = await _service.GetPortfolioDTOsAsync(userID);

        return CheckReturn(dtos);
    }

    // GET: api/PortfolioBenchmark?portfolioId={portfolioId}
    /// <summary>
    /// Gets a list of portfolio-benchmark associations for the authenticated user.
    /// </summary>
    /// <returns>A list of PortfolioBenchmarkDTO objects containing the portfolio-benchmark associations for the authenticated user.</returns>
    /// <remarks>
    /// Sample request:
    /// <code>
    /// GET /api/PortfolioBenchmark
    /// Authorization: Bearer {token}
    /// </code>
    /// </remarks>
    /// <response code="200">Returns the portfolio-benchmark associations</response>
    /// <response code="404">If no portfolio-benchmark associations are found for the user</response>
    [HttpGet("/api/PortfolioBenchmark")]
    [ProducesResponseType(typeof(IEnumerable<PortfolioBenchmarkDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<PortfolioBenchmarkDTO>>> GetPortfolioBenchmarks()
    {
        var userID = GetUserId();
        if (userID == null)
        {
            return Ok(new List<PortfolioBenchmarkDTO>());
        }

        var dtos = await _service.GetPortfolioBenchmarksAsync(userID);

        return CheckReturn(dtos);
    }
}
