using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PerformanceApp.Server.Dtos;
using PerformanceApp.Server.Services;

namespace PerformanceApp.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class DateInfoController(IDateInfoService service) : MyControllerBase
{
    private readonly IDateInfoService _service = service;

    // GET: api/DateInfo
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BankdayDTO>>> GetDates()
    {
        if (!UserIsAuthenticated())
        {
            return UnauthorizedResponse();
        }

        var bankdayDtos = await _service.GetBankdayDTOsAsync();

        if (bankdayDtos == null || bankdayDtos.Count == 0)
        {
            return NotFound();
        }
        return Ok(bankdayDtos);
    }

}