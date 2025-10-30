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
            var result = await (
                from bm in _context.Benchmark
                join p in _context.Portfolio
                    on bm.PortfolioId equals p.PortfolioId
                join b in _context.Portfolio
                    on bm.BenchmarkId equals b.PortfolioId
                select new PortfolioBenchmarkDTO
                {
                    PortfolioID = p.PortfolioId,
                    PortfolioName = p.PortfolioName,
                    BenchmarkID = b.PortfolioId,
                    BenchmarkName = b.PortfolioName
                }
            ).ToListAsync();

            if (result == null)
            {
                return NotFound();
            }

            return result;
        }
    }
}