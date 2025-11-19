using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PerformanceApp.Server.Controllers;
using PerformanceApp.Server.Dtos;
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
        return Enumerable.Range(0, count)
            .Select(_ => CreateDto())
            .ToList();
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
        AssertIsOk(result, keyFigures.Count);
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
        AssertIsNotFound(result);
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
        AssertIsUnauthorized(result);
    }

}