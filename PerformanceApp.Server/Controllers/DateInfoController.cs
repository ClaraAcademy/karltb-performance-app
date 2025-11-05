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
    public class DateInfoController : ControllerBase
    {
        private readonly IDateInfoService _service;

        public DateInfoController(IDateInfoService service)
        {
            _service = service;
        }

        // GET: api/DateInfo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BankdayDTO>>> GetDates()
        {
            var bankdayDtos = await _service.GetBankdayDTOsAsync();

            if (bankdayDtos == null || bankdayDtos.Count == 0)
            {
                return NotFound();
            }
            return Ok(bankdayDtos);
        }

    }
}