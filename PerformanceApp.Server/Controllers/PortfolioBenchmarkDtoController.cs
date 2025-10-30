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
            var result = await (
                from bm in _context.Benchmark
                join p in _context.Portfolio
                    on bm.PortfolioID equals p.PortfolioID
                join b in _context.Portfolio
                    on bm.BenchmarkID equals b.PortfolioID
                select new PortfolioBenchmarkDTO
                {
                    PortfolioID = p.PortfolioID,
                    PortfolioName = p.PortfolioName,
                    BenchmarkID = b.PortfolioID,
                    BenchmarkName = b.PortfolioName
                }
            ).ToListAsync();

            if (result == null)
            {
                return NotFound();
            }

            return result;
        }

        // GET: api/PortfolioBenchmarkDTO/id
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<PortfolioBenchmarkDTO>>> GetPortfolioBenchmarks(int id)
        {
            var result = await (
                from bm in _context.Benchmark
                where bm.PortfolioID == id
                join p in _context.Portfolio
                    on bm.PortfolioID equals p.PortfolioID
                join b in _context.Portfolio
                    on bm.BenchmarkID equals b.PortfolioID
                select new PortfolioBenchmarkDTO
                {
                    PortfolioID = p.PortfolioID,
                    PortfolioName = p.PortfolioName,
                    BenchmarkID = b.PortfolioID,
                    BenchmarkName = b.PortfolioName
                }
            ).ToListAsync();

            if (result == null || result.Count == 0)
            {
                return NotFound();
            }

            return result;
        }
    }
}