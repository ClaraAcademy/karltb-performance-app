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

        // GET: /api/position/stocks?portfolioId=123&date=2025-10-31
        [HttpGet("stocks")]
        public async Task<ActionResult<IEnumerable<StockPositionDTO>>> GetStockPositions(
            [FromQuery] int portfolioId,
            [FromQuery] string date
        )
        {
            var bankday = DateOnly.Parse(date);
            var sp = await _context.Positions
                .Where(p => p.Bankday == bankday
                         && p.PortfolioId == portfolioId
                         && p.Instrument.InstrumentType.InstrumentTypeName == "Stock")
                .Select(p => new StockPositionDTO
                {
                    PortfolioId = p.PortfolioId,
                    InstrumentId = p.InstrumentId,
                    InstrumentName = p.Instrument.InstrumentName,
                    Bankday = p.Bankday,
                    Value = p.PositionValues.SingleOrDefault(pv => pv.Bankday == p.Bankday).PositionValue1,
                    UnitPrice = p.Instrument.InstrumentPrices.SingleOrDefault(ip => ip.Bankday == p.Bankday).Price,
                    Count = p.Count
                })
                .ToListAsync();

            if (sp == null || sp.Count == 0)
            {
                return NotFound();
            }
            return Ok(sp);
        }

        // GET: /api/position/bonds?portfolioId=123&date=2025-10-31
        [HttpGet("bonds")]
        public async Task<ActionResult<IEnumerable<StockPositionDTO>>> GetBondPositions(
            [FromQuery] int portfolioId,
            [FromQuery] string date
        )
        {
            var bankday = DateOnly.Parse(date);
            var sp = await _context.Positions
                .Where(p => p.Bankday == bankday
                         && p.PortfolioId == portfolioId
                         && p.Instrument.InstrumentType.InstrumentTypeName == "Bond")
                .Select(p => new BondPositionDTO
                {
                    PortfolioId = p.PortfolioId,
                    InstrumentId = p.InstrumentId,
                    InstrumentName = p.Instrument.InstrumentName,
                    Bankday = p.Bankday,
                    Value = p.PositionValues.SingleOrDefault(pv => pv.Bankday == p.Bankday).PositionValue1,
                    UnitPrice = p.Instrument.InstrumentPrices.SingleOrDefault(ip => ip.Bankday == p.Bankday).Price,
                    Nominal = p.Nominal
                })
                .ToListAsync();

            if (sp == null || sp.Count == 0)
            {
                return NotFound();
            }
            return Ok(sp);
        }

        // GET: /api/position/indexes?portfolioId=123&date=2025-10-31
        [HttpGet("indexes")]
        public async Task<ActionResult<IEnumerable<StockPositionDTO>>> GetIndexPositions(
            [FromQuery] int portfolioId,
            [FromQuery] string date
        )
        {
            var bankday = DateOnly.Parse(date);
            var sp = await _context.Positions
                .Where(p => p.Bankday == bankday
                         && p.PortfolioId == portfolioId
                         && p.Instrument.InstrumentType.InstrumentTypeName == "Index")
                .Select(p => new IndexPositionDTO
                {
                    PortfolioId = p.PortfolioId,
                    InstrumentId = p.InstrumentId,
                    InstrumentName = p.Instrument.InstrumentName,
                    Bankday = p.Bankday,
                    Value = p.PositionValues.SingleOrDefault(pv => pv.Bankday == p.Bankday).PositionValue1,
                    UnitPrice = p.Instrument.InstrumentPrices.SingleOrDefault(ip => ip.Bankday == p.Bankday).Price,
                    Proportion = p.Proportion
                })
                .ToListAsync();

            if (sp == null || sp.Count == 0)
            {
                return NotFound();
            }
            return Ok(sp);
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