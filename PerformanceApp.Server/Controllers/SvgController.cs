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
    public class SvgController(ISvgService service) : ControllerBase
    {
        private readonly ISvgService _service = service;


        // Get: /api/svg?portfolioId={portfolioID}&width={width}&height={height}&border={border}
        [HttpGet]
        public async Task<ActionResult<string>> GetCumulativePerformanceGraph(
            [FromQuery] int portfolioId,
            [FromQuery] int? width = null,
            [FromQuery] int? height = null,
            [FromQuery] int? border = null
        )
        {
            var svg = await _service.GetLineChart(portfolioId, width, height, border);

            if (svg == null)
            {
                return NotFound();
            }

            return Content(svg, "image/svg+xml");
        }
    }
}