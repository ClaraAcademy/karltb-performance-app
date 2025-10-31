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
    public class PositionController : ControllerBase
    {
        private readonly PadbContext _context;

        public PositionController(PadbContext context)
        {
            _context = context;
        }

        // Get: api/Position
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Position>>> GetPositions()
        {
            var positions = await _context.Positions
                .OrderBy(p => p.Bankday)
                .ThenBy(p => p.PortfolioId)
                .ThenBy(p => p.InstrumentId)
                .ToListAsync();

            if (positions == null)
            {
                return NotFound();
            }

            return Ok(positions);
        }

        // Get: api/Position/{date} (date as yyyy-mm-dd)
        [HttpGet("{date}")]
        public async Task<ActionResult<IEnumerable<Position>>> GetPositions(string date)
        {
            if (!DateOnly.TryParse(date, out var parsed))
            {
                return BadRequest("Invalid date format. Use yyyy-mm-dd");
            }

            var start = parsed;
            var end = start.AddDays(1);

            var positions = await _context.Positions
                .Where(p =>
                    p.Bankday >= start 
                    && p.Bankday < end
                )
                .OrderBy(p => p.Bankday)
                .ThenBy(p => p.PortfolioId)
                .ThenBy(p => p.InstrumentId)
                .ToListAsync();

            if (positions == null)
            {
                return NotFound();
            }

            return Ok(positions);
        }

        // Get: api/Position/{date}/{portfolioId} (date as yyyy-mm-dd)
        [HttpGet("{date}/{portfolioId}")]
        public async Task<ActionResult<IEnumerable<Position>>> GetPositions(string date, int portfolioId)
        {
            if (!DateOnly.TryParse(date, out var parsed))
            {
                return BadRequest("Invalid date format. Use yyyy-mm-dd");
            }

            var start = parsed;
            var end = start.AddDays(1);

            var positions = await _context.Positions
                .Where(p =>
                    p.Bankday >= start 
                    && p.Bankday < end 
                    && p.PortfolioId == portfolioId
                )
                .OrderBy(p => p.Bankday)
                .ThenBy(p => p.InstrumentId)
                .ToListAsync();

            if (positions == null)
            {
                return NotFound();
            }

            return Ok(positions);
        }
    }

}