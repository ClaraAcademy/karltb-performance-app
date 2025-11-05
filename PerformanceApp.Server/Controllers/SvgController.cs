using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Identity.Client.AppConfig;
using NuGet.Common;
using PerformanceApp.Server.Data;
using PerformanceApp.Server.Models;
using SQLitePCL;
using PerformanceApp.Server.Services;

namespace PerformanceApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SvgController : ControllerBase
    {
        private readonly PadbContext _context;

        public SvgController(PadbContext context)
        {
            _context = context;
        }

        private ActionResult CheckReturn<T>(T? res)
        {
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }
        private IQueryable<PortfolioCumulativeDayPerformanceDTO> GetPortfolioPerformance(int portfolioId)
        {
            return _context.PortfolioCumulativeDayPerformances
                .Where(pv => pv.Portfolio.PortfolioId == portfolioId)
                .OrderBy(pv => pv.BankdayNavigation.Bankday)
                .Select(
                    pv => new PortfolioCumulativeDayPerformanceDTO
                    {
                        Bankday = pv.BankdayNavigation.Bankday,
                        Value = pv.CumulativeDayPerformance ?? 0
                    }
                );
        }

        [HttpGet]
        // Get: /api/svg?portfolioId={portfolioID}&benchmarkId={benchmarkId}
        public async Task<ActionResult<string>> GetCumulativePerformanceGraph(
            [FromQuery] int portfolioId,
            [FromQuery] int benchmarkId
        )
        {
            var portfolioPerformance = GetPortfolioPerformance(portfolioId);
            var benchmarkPerformance = GetPortfolioPerformance(benchmarkId);

            var p = await portfolioPerformance
                .Join(benchmarkPerformance,
                    pp => pp.Bankday,
                    bp => bp.Bankday,
                    (pp, bp) => new PortfolioBenchmarkCumulativeDayPerformanceDTO
                    {
                        Bankday = pp.Bankday,
                        PortfolioValue = pp.Value,
                        BenchmarkValue = bp.Value
                    }
                ).ToListAsync();

            if (p == null || p.Count == 0)
            {
                return NotFound();
            }

            var svg = Plot.CreateSVG(p);
            return Content(svg, "image/svg+xml");
        }
    }
}