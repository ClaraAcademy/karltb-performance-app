using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PerformanceApp.Server.Dtos;
using PerformanceApp.Server.Services;

namespace PerformanceApp.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PerformanceController(IPerformanceService service) : MyControllerBase
{
    private readonly IPerformanceService Service = service;

    private ActionResult CheckReturn<T>(List<T>? values)
    {
        var valid = values != null && values.Count > 0;
        if (valid)
        {
            return Ok(values);
        }
        return NotFound();
    }

    // GET: api/performance?portfolioId={portfolioId}
    [HttpGet]
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