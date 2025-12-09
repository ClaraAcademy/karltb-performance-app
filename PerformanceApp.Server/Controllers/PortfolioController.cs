using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PerformanceApp.Server.Dtos;
using PerformanceApp.Server.Services;

namespace PerformanceApp.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PortfolioController(IPortfolioService service) : MyControllerBase
{
    private readonly IPortfolioService _service = service;

    private ActionResult CheckReturn<T>(List<T>? ps)
    {
        if (ps == null || ps.Count == 0)
        {
            return NotFound();
        }
        return Ok(ps);
    }

    private string? GetUserId()
    {
        return User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }

    // GET: api/Portfolio
    [HttpGet]
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
    [HttpGet("/api/PortfolioBenchmark")]
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
