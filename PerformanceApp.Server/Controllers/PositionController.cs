using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PerformanceApp.Server.Data;
using PerformanceApp.Server.Models;
using PerformanceApp.Server.Services;
using SQLitePCL;

namespace PerformanceApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionController(IPositionService service) : ControllerBase
    {
        private readonly IPositionService _service = service;

        private ActionResult CheckReturn<T>(List<T>? ps)
            => ps == null || ps.Count == 0 ? NotFound() : Ok(ps);

        // GET: /api/position/stocks&portfolioId=123&date=2025-10-31
        [HttpGet("stocks")]
        public async Task<ActionResult<List<StockPositionDTO>>> GetStockPositions([FromQuery] int portfolioId, [FromQuery] string date)
            => CheckReturn(await _service.GetStockPositionsAsync(DateOnly.Parse(date), portfolioId));

        // GET: /api/position/bonds&portfolioId=123&date=2025-10-31
        [HttpGet("bonds")]
        public async Task<ActionResult<List<BondPositionDTO>>> GetBondPositions([FromQuery] int portfolioId, [FromQuery] string date)
            => CheckReturn(await _service.GetBondPositionsAsync(DateOnly.Parse(date), portfolioId));

        // GET: /api/position/indexes&portfolioId=123&date=2025-10-31
        [HttpGet("indexes")]
        public async Task<ActionResult<List<IndexPositionDTO>>> GetIndexPositions([FromQuery] int portfolioId, [FromQuery] string date)
            => CheckReturn(await _service.GetIndexPositionsAsync(DateOnly.Parse(date), portfolioId));

    }

}