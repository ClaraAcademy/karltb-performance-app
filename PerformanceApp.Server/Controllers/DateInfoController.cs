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
    /// <summary>
    /// Gets a list of bankdays in the database.
    /// </summary>
    /// <returns>A list of BankdayDTO objects containing the bankdays in the database.</returns>
    /// <remarks>
    /// Sample request:
    /// 
    ///     GET /api/dateinfo
    ///     Authorization: Bearer {token}
    /// 
    /// </remarks>
    /// <response code="200">Returns the list of bankdays</response>
    /// <response code="401">If the user is not authenticated</response>
    /// <response code="404">If no bankdays are found</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<BankdayDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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