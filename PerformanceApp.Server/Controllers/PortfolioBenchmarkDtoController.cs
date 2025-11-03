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

        private ActionResult CheckReturn<T>(List<T>? ps)
        {
            if (ps == null || ps.Count == 0)
            {
                return NotFound();
            }
            return Ok(ps);
        }

        private async Task<List<PortfolioBenchmarkDTO>> GetPortfolioBenchmarkDTO(int? portfolioId = null)
        {
            IQueryable<Benchmark> q = _context.Benchmarks;

            if (portfolioId.HasValue)
            {
                q = q.Where(bm => bm.PortfolioId == portfolioId);
            }

            return await q.Select(
                bm => new PortfolioBenchmarkDTO
                {
                    PortfolioId = bm.Portfolio.PortfolioId,
                    PortfolioName = bm.Portfolio.PortfolioName,
                    BenchmarkId = bm.BenchmarkNavigation.PortfolioId,
                    BenchmarkName = bm.BenchmarkNavigation.PortfolioName
                }
            ).ToListAsync();
        }

        // GET: api/PortfolioBenchmarkDTO
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PortfolioBenchmarkDTO>>> GetPortfoliosBenchmarks()
        {
            return CheckReturn(
                await GetPortfolioBenchmarkDTO()
            );
        }

        // GET: api/PortfolioBenchmarkDTO/portfolioId
        [HttpGet("{portfolioId}")]
        public async Task<ActionResult<IEnumerable<PortfolioBenchmarkDTO>>> GetPortfolioBenchmarks(int portfolioId)
        {
            return CheckReturn(
                await GetPortfolioBenchmarkDTO(portfolioId)
            );
        }
    }
}