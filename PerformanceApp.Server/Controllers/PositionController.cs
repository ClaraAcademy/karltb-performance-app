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

        private async Task<List<T>> GetPositions<T>(
            int portfolioId,
            string date,
            string instrumentTypeName,
            Func<Position, T> selector
        )
        {
            var bankday = DateOnly.Parse(date);
            return await _context.Positions
                .Include(p => p.Instrument!)
                    .ThenInclude(i => i.InstrumentType)
                .Include(p => p.Instrument!)
                    .ThenInclude(i => i.InstrumentPrices)
                .Include(p => p.PositionValues)
                .Where(p => p.Bankday == bankday)
                .Where(p => p.PortfolioId == portfolioId)
                .Where(p =>
                    p.Instrument!.InstrumentType!.InstrumentTypeName
                    == instrumentTypeName
                )
                .Select(p => selector(p))
                .ToListAsync();

        }

        private decimal? GetPositionValue(Position p)
        {
            return p.PositionValues?.SingleOrDefault(pv => pv.Bankday == p.Bankday)?.PositionValue1;
        }

        private decimal? GetInstrumentUnitPrice(Position p)
        {
            return p.Instrument?.InstrumentPrices?.SingleOrDefault(ip => ip.Bankday == p.Bankday)?.Price;
        }

        private StockPositionDTO SelectStockPosition(Position p)
        {
            return new StockPositionDTO
            {
                PortfolioId = p.PortfolioId,
                InstrumentId = p.InstrumentId,
                InstrumentName = p.Instrument?.InstrumentName,
                Bankday = p.Bankday,
                Value = GetPositionValue(p),
                UnitPrice = GetInstrumentUnitPrice(p),
                Count = p.Count
            };
        }

        private BondPositionDTO SelectBondPosition(Position p)
        {
            return new BondPositionDTO
            {
                PortfolioId = p.PortfolioId,
                InstrumentId = p.InstrumentId,
                InstrumentName = p.Instrument?.InstrumentName,
                Bankday = p.Bankday,
                Value = GetPositionValue(p),
                UnitPrice = GetInstrumentUnitPrice(p),
                Nominal = p.Nominal
            };
        }

        private IndexPositionDTO SelectIndexPosition(Position p)
        {
            return new IndexPositionDTO
            {
                PortfolioId = p.PortfolioId,
                InstrumentId = p.InstrumentId,
                InstrumentName = p.Instrument?.InstrumentName,
                Bankday = p.Bankday,
                Value = GetPositionValue(p),
                UnitPrice = GetInstrumentUnitPrice(p),
                Proportion = p.Proportion
            };
        }

        private ActionResult CheckReturn<T>(List<T>? ps)
            => ps == null || ps.Count == 0 ? NotFound() : Ok(ps);


        // GET: /api/position/stocks?portfolioId=123&date=2025-10-31
        [HttpGet("stocks")]
        public async Task<ActionResult<IEnumerable<StockPositionDTO>>> GetStockPositions(
            [FromQuery] int portfolioId,
            [FromQuery] string date
        )
        {
            return CheckReturn(
                await GetPositions(
                    portfolioId, date, "Stock", SelectStockPosition
                )
            );
        }

        // GET: /api/position/bonds?portfolioId=123&date=2025-10-31
        [HttpGet("bonds")]
        public async Task<ActionResult<IEnumerable<BondPositionDTO>>> GetBondPositions(
            [FromQuery] int portfolioId,
            [FromQuery] string date
        )
        {
            return CheckReturn(
                await GetPositions(
                    portfolioId, date, "Bond", SelectBondPosition
                )
            );
        }

        // GET: /api/position/indexes?portfolioId=123&date=2025-10-31
        [HttpGet("indexes")]
        public async Task<ActionResult<IEnumerable<IndexPositionDTO>>> GetIndexPositions(
            [FromQuery] int portfolioId,
            [FromQuery] string date
        )
        {
            return CheckReturn(
                await GetPositions(
                    portfolioId, date, "Index", SelectIndexPosition
                )
            );
        }
    }

}