using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PerformanceApp.Server.Data;
using PerformanceApp.Server.Models;
using SQLitePCL;

namespace PerformanceApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortfoliosController : ControllerBase
    {
        private readonly PadbContext _context;

        public PortfoliosController(PadbContext context)
        {
            _context = context;
        }

        // GET: api/Portfolios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Portfolio>>> GetPortfolios()
        {
            var portfolios = await _context.Portfolio
                .Where(
                    p => _context.Benchmark.Any(
                        b => b.PortfolioID == p.PortfolioID
                    )
                ).ToListAsync();

            if (portfolios == null)
            {
                return NotFound();
            }

            return Ok(portfolios);
        }
    }
}
