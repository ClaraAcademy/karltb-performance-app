using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PerformanceApp.Server.DTOs;
using PerformanceApp.Server.Services;
using SQLitePCL;

namespace PerformanceApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortfolioController(IPortfolioService service) : ControllerBase
    {
        private readonly IPortfolioService _service = service;

        private ActionResult CheckReturn<T>(List<T>? ps)
        => ps == null || ps.Count == 0 ? NotFound() : Ok(ps);

        // GET: api/Portfolio
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PortfolioDTO>>> GetPortfolios()
        => CheckReturn(await _service.GetPortfolioDTOsAsync());

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
