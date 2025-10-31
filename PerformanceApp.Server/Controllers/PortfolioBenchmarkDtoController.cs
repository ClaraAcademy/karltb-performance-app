using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PerformanceApp.Server.Data;
using PerformanceApp.Server.Models;
namespace PerformanceApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortfolioBenchmarkDtoController : ControllerBase
    {
        private readonly PadbContext _context;

        public PortfolioBenchmarkDtoController(PadbContext context)
        {
            _context = context;
        }

        // GET: api/PortfolioBenchmarkDTO
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PortfolioBenchmarkDTO>>> GetPortfoliosBenchmarks()
        {
            var pbs = await _context.Benchmarks.Select(
                bm => new PortfolioBenchmarkDTO
                {
                    PortfolioId = bm.Portfolio.PortfolioId,
                    PortfolioName = bm.Portfolio.PortfolioName,
                    BenchmarkId = bm.BenchmarkNavigation.PortfolioId,
                    BenchmarkName = bm.BenchmarkNavigation.PortfolioName
                }
            ).ToListAsync();

            if (pbs == null)
            {
                return NotFound();
            }

            return pbs;
        }

        // GET: api/PortfolioBenchmarkDTO/id
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<PortfolioBenchmarkDTO>>> GetPortfolioBenchmarks(int id)
        {
            var pbs = await _context.Benchmarks
            .Where(bm => bm.PortfolioId == id)
            .Select(
                bm => new PortfolioBenchmarkDTO
                {
                    PortfolioId = bm.Portfolio.PortfolioId,
                    PortfolioName = bm.Portfolio.PortfolioName,
                    BenchmarkId = bm.BenchmarkNavigation.PortfolioId,
                    BenchmarkName = bm.BenchmarkNavigation.PortfolioName
                }
            ).ToListAsync();

            if (pbs == null)
            {
                return NotFound();
            }

            return pbs;
        }
    }
}