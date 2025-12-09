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