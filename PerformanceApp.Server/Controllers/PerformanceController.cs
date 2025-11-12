using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PerformanceApp.Server.DTOs;
using PerformanceApp.Server.Services;
using SQLitePCL;

namespace PerformanceApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerformanceController(IPerformanceService service) : ControllerBase
    {
        private readonly IPerformanceService Service = service;

        private ActionResult CheckReturn<T>(List<T>? values)
            => values == null || values.Count == 0 ? NotFound() : Ok(values);

        // GET: api/performance?portfolioId={portfolioId}
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PortfolioBenchmarkKeyFigureDTO>>> GetKeyFigures([FromQuery] int portfolioId)
            => CheckReturn(await Service.GetPortfolioBenchmarkKeyFigureValues(portfolioId));
    }
}