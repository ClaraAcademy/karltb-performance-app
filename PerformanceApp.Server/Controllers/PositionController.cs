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

    // GET: /api/position/stocks&portfolioId=123&date=2025-10-31
    [HttpGet("stocks")]
    public async Task<ActionResult<List<StockPositionDTO>>> GetStockPositions(
        [FromQuery] int portfolioId, [FromQuery] string date
    )
    {
        var method = _service.GetStockPositionsAsync;
        return await GetPositionsAsync(method, portfolioId, date);
    }

    // GET: /api/position/bonds&portfolioId=123&date=2025-10-31
    [HttpGet("bonds")]
    public async Task<ActionResult<List<BondPositionDTO>>> GetBondPositions(
        [FromQuery] int portfolioId, [FromQuery] string date
    )
    {
        var method = _service.GetBondPositionsAsync;
        return await GetPositionsAsync(method, portfolioId, date);
    }

    // GET: /api/position/indexes&portfolioId=123&date=2025-10-31
    [HttpGet("indexes")]
    public async Task<ActionResult<List<IndexPositionDTO>>> GetIndexPositions(
        [FromQuery] int portfolioId, [FromQuery] string date
    )
    {
        var method = _service.GetIndexPositionsAsync;
        return await GetPositionsAsync(method, portfolioId, date);
    }
}
