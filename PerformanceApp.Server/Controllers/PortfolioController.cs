using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PerformanceApp.Server.Dtos;
using PerformanceApp.Server.Services;

namespace PerformanceApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PortfolioController(IPortfolioService service) : MyControllerBase
    {
        private readonly IPortfolioService _service = service;

        private ActionResult CheckReturn<T>(List<T>? ps)
        => ps == null || ps.Count == 0 ? NotFound() : Ok(ps);

        // GET: api/Portfolio
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PortfolioDTO>>> GetPortfolios()
        {
            var dtos = await _service.GetPortfolioDTOsAsync();

            return CheckReturn(dtos);
        }

        // GET: api/PortfolioBenchmark?portfolioId={portfolioId}
        [HttpGet("/api/PortfolioBenchmark")]
        public async Task<ActionResult<IEnumerable<PortfolioBenchmarkDTO>>> GetPortfolioBenchmarks([FromQuery] int? portfolioId)
        {
            var result = portfolioId == null
                ? await _service.GetPortfolioBenchmarksAsync()
                : await _service.GetPortfolioBenchmarksAsync(portfolioId.Value);

            return CheckReturn(result);
        }
    }
}
