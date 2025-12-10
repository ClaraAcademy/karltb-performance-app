using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PerformanceApp.Server.Dtos;
using PerformanceApp.Server.Services;

namespace PerformanceApp.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PositionController(IPositionService service) : ControllerBase
{
    private readonly IPositionService _service = service;

    private async Task<ActionResult<List<T>>> GetPositionsAsync<T>(
        Func<DateOnly, int, Task<List<T>>> serviceMethod, int portfolioId, string date
    )
    {
        var dateOnly = DateOnly.Parse(date);
        var dtos = await serviceMethod(dateOnly, portfolioId);
        return Ok(dtos);
    }

    /// <summary>
    /// Gets a list of stock positions for a specific portfolio on a given date.
    /// </summary>
    /// <param name="portfolioId">The ID of the portfolio.</param>
    /// <param name="date">The date for which to retrieve the positions.</param>
    /// <returns>A list of StockPositionDTO objects representing the stock positions.</returns>
    /// <remarks>
    /// Sample request:
    /// <code>
    /// GET /api/position/stocks?portfolioId=123&amp;date=2025-10-31
    /// Authorization: Bearer {token}
    /// </code>
    /// </remarks>
    /// <response code="200">Returns the list of stock positions</response>
    [HttpGet("stocks")]
    [ProducesResponseType(typeof(List<StockPositionDTO>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<StockPositionDTO>>> GetStockPositions(
        [FromQuery] int portfolioId, [FromQuery] string date
    )
    {
        var method = _service.GetStockPositionsAsync;
        return await GetPositionsAsync(method, portfolioId, date);
    }

    /// <summary>
    /// Gets a list of bond positions for a specific portfolio on a given date.
    /// </summary>
    /// <param name="portfolioId">The ID of the portfolio.</param>
    /// <param name="date">The date for which to retrieve the positions.</param>
    /// <returns>A list of BondPositionDTO objects representing the bond positions.</returns>
    /// <remarks>
    /// Sample request:
    /// <code>
    /// GET /api/position/bonds?portfolioId=123&amp;date=2025-10-31
    /// Authorization: Bearer {token}
    /// </code>
    /// </remarks>
    /// <response code="200">Returns the list of bond positions</response>
    [HttpGet("bonds")]
    [ProducesResponseType(typeof(List<BondPositionDTO>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<BondPositionDTO>>> GetBondPositions(
        [FromQuery] int portfolioId, [FromQuery] string date
    )
    {
        var method = _service.GetBondPositionsAsync;
        return await GetPositionsAsync(method, portfolioId, date);
    }

    /// <summary>
    /// Gets a list of index positions for a specific portfolio on a given date.
    /// </summary>
    /// <param name="portfolioId">The ID of the portfolio.</param>
    /// <param name="date">The date for which to retrieve the positions.</param>
    /// <returns>A list of IndexPositionDTO objects representing the index positions.</returns>
    /// <remarks>
    /// Sample request:
    /// <code>
    /// GET /api/position/indexes?portfolioId=123&amp;date=2025-10-31
    /// Authorization: Bearer {token}
    /// </code>
    /// </remarks>
    /// <response code="200">Returns the list of index positions</response>
    [HttpGet("indexes")]
    [ProducesResponseType(typeof(List<IndexPositionDTO>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<IndexPositionDTO>>> GetIndexPositions(
        [FromQuery] int portfolioId, [FromQuery] string date
    )
    {
        var method = _service.GetIndexPositionsAsync;
        return await GetPositionsAsync(method, portfolioId, date);
    }
}
