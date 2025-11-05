using System;
using System.Collections.Generic;
using System.Linq;
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
    public class PortfoliosController : ControllerBase
    {
        private readonly IPortfolioService _service;

        public PortfoliosController(IPortfolioService service)
        {
            _service = service;
        }

        // GET: api/Portfolios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PortfolioDTO>>> GetPortfolios()
        {
            var portfolios = await _service.GetPortfolioDTOsAsync();

            if (portfolios == null || portfolios.Count == 0)
            {
                return NotFound();
            }

            return Ok(portfolios);
        }
    }
}
