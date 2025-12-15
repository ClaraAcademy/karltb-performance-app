using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PerformanceApp.Data.Dtos;
using PerformanceApp.Server.Controllers;
using PerformanceApp.Server.Services;

namespace PerformanceApp.Server.Test.Controllers;

public class PerformanceControllerTest : ControllerTestBase
{
    private readonly Mock<IPerformanceService> _mockPerformanceService;

    public PerformanceControllerTest()
    {
        _mockPerformanceService = new Mock<IPerformanceService>();
    }

    private PortfolioBenchmarkKeyFigureDTO CreateDto()
    {
        return new PortfolioBenchmarkKeyFigureDTO
        {
            KeyFigureId = Random.Shared.Next(1, 1000),
            KeyFigureName = "Return",
            PortfolioId = 1,
            PortfolioName = "Sample Portfolio",
            PortfolioValue = Random.Shared.Next(0, 100),
            BenchmarkId = 2,
            BenchmarkName = "Sample Benchmark",
            BenchmarkValue = Random.Shared.Next(0, 100)
        };
    }

    private List<PortfolioBenchmarkKeyFigureDTO> CreateDtos(int count)
    {
        return Enumerable.Range(0, count).Select(_ => CreateDto()).ToList();
    }

    [Fact]
    public async Task GetKeyFigures_ReturnsOk_WhenDataExists()
    {
        // Arrange
        int portfolioId = 1;
        var keyFigures = CreateDtos(1);

        _mockPerformanceService.Setup(s => s.GetPortfolioBenchmarkKeyFigureValues(portfolioId))
            .ReturnsAsync(keyFigures);

        var controller = new PerformanceController(_mockPerformanceService.Object);

        var user = CreateUserPrincipal("testuser");

        controller.ControllerContext = CreateControllerContext(user);

        // Act
        var result = await controller.GetKeyFigures(portfolioId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(StatusCodes.OK, okResult.StatusCode);
        var returnedKeyFigures = Assert.IsType<List<PortfolioBenchmarkKeyFigureDTO>>(okResult.Value);
        Assert.Equal(keyFigures.Count, returnedKeyFigures.Count);
    }

    [Fact]
    public async Task GetKeyFigures_ReturnsNotFound_WhenNoDataExists()
    {
        // Arrange
        int portfolioId = 1;

        _mockPerformanceService.Setup(s => s.GetPortfolioBenchmarkKeyFigureValues(portfolioId))
            .ReturnsAsync(new List<PortfolioBenchmarkKeyFigureDTO>());

        var controller = new PerformanceController(_mockPerformanceService.Object);

        var user = CreateUserPrincipal("testuser");

        controller.ControllerContext = CreateControllerContext(user);

        // Act
        var result = await controller.GetKeyFigures(portfolioId);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundResult>(result.Result);
        Assert.Equal(StatusCodes.NotFound, notFoundResult.StatusCode);
    }

    [Fact]
    public async Task GetKeyFigures_ReturnsUnauthorized_WhenUserIsNotAuthenticated()
    {
        // Arrange
        int portfolioId = 1;

        var controller = new PerformanceController(_mockPerformanceService.Object);
        // Simulate unauthenticated user
        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext()
        };

        // Act
        var result = await controller.GetKeyFigures(portfolioId);

        // Assert
        var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result.Result);
        Assert.Equal(StatusCodes.Unauthorized, unauthorizedResult.StatusCode);
    }

}