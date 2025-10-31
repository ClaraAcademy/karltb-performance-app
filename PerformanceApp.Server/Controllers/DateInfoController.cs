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
    public class DateInfoController : ControllerBase
    {
        private readonly PadbContext _context;

        public DateInfoController(PadbContext context)
        {
            _context = context;
        }

        // GET: api/DateInfo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DateInfo>>> GetDates()
        {
            var dates = await _context.DateInfo
                .Distinct()
                .OrderBy(d => d.Bankday)
                .ToListAsync();

            if (dates == null)
            {
                return NotFound();
            }

            return Ok(dates);
        }

    }
}