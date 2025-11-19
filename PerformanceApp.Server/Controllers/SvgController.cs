using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PerformanceApp.Server.Services;

namespace PerformanceApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SvgController(ISvgService service) : ControllerBase
    {
        private readonly ISvgService _service = service;


        // Get: /api/svg?portfolioId={portfolioID}&width={width}&height={height}
        [HttpGet]
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
}