using PerformanceApp.Server.Controllers;
using PerformanceApp.Server.Dtos;
using PerformanceApp.Server.Services;
using Moq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
namespace PerformanceApp.Server.Test.Controllers;

public class DateInfoControllerTest : ControllerTestBase
{
    private readonly Mock<IDateInfoService> _mockDateInfoService;
    public DateInfoControllerTest()
    {
        _mockDateInfoService = new Mock<IDateInfoService>();
    }

    private static List<BankdayDTO> CreateBankdayDtos()
    {
        int count = 10;
        return Enumerable.Range(1, count)
            .Select(i => new BankdayDTO { Bankday = new DateOnly(2024, 1, i) })
            .ToList();
    }

    [Fact]
    public async Task GetDates_ReturnsOk_WhenDataExists()
    {
        // Arrange
        _mockDateInfoService.Setup(s => s.GetBankdayDTOsAsync())
            .ReturnsAsync(CreateBankdayDtos());

        var controller = new DateInfoController(_mockDateInfoService.Object);

        var user = CreateUserPrincipal("testuser");

        controller.ControllerContext = CreateControllerContext(user);

        // Act
        var result = await controller.GetDates();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task GetDates_ReturnsNotFound_WhenNoDataExists()
    {
        // Arrange
        _mockDateInfoService.Setup(s => s.GetBankdayDTOsAsync())
            .ReturnsAsync(new List<BankdayDTO>());

        var controller = new DateInfoController(_mockDateInfoService.Object);

        var user = CreateUserPrincipal("testuser");

        controller.ControllerContext = CreateControllerContext(user);

        // Act
        var result = await controller.GetDates();

        // Assert
        var notFoundResult = Assert.IsType<NotFoundResult>(result.Result);
    }
}