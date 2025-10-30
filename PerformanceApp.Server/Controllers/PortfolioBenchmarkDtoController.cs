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
            var pbs = await _context.Benchmark.Select(
                bm => new PortfolioBenchmarkDTO
                {
                    PortfolioID = bm.Portfolio.PortfolioID,
                    PortfolioName = bm.Portfolio.PortfolioName,
                    BenchmarkID = bm.BenchmarkPortfolio.PortfolioID,
                    BenchmarkName = bm.BenchmarkPortfolio.PortfolioName
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
            var pbs = await _context.Benchmark
            .Where(bm => bm.PortfolioID == id)
            .Select(
                bm => new PortfolioBenchmarkDTO
                {
                    PortfolioID = bm.Portfolio.PortfolioID,
                    PortfolioName = bm.Portfolio.PortfolioName,
                    BenchmarkID = bm.BenchmarkPortfolio.PortfolioID,
                    BenchmarkName = bm.BenchmarkPortfolio.PortfolioName
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