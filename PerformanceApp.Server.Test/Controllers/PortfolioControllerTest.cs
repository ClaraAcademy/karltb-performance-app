using Microsoft.AspNetCore.Mvc;
using Moq;
using PerformanceApp.Data.Seeding.Constants;
using PerformanceApp.Server.Controllers;
using PerformanceApp.Server.Dtos;
using PerformanceApp.Server.Services;

namespace PerformanceApp.Server.Test.Controllers;

public class PortfolioControllerTest : ControllerTestBase
{
    private readonly Mock<IPortfolioService> _mockPortfolioService;
    public PortfolioControllerTest()
    {
        _mockPortfolioService = new Mock<IPortfolioService>();
    }

    private PortfolioDTO CreatePortfolioDTO(int i)
    {
        return new PortfolioDTO { PortfolioId = i, PortfolioName = $"Portfolio {i}" };
    }

    private List<PortfolioDTO> CreatePortfolioDTOs(int count)
    {
        return Enumerable.Range(1, count)
            .Select(CreatePortfolioDTO)
            .ToList();
    }

    [Fact]
    public async Task GetPortfolios_ReturnsOk_WhenDataExists()
    {
        // Arrange
        var portfolios = CreatePortfolioDTOs(2);

        _mockPortfolioService.Setup(s => s.GetPortfolioDTOsAsync(UserData.UsernameB))
            .ReturnsAsync(portfolios);


        var controller = new PortfolioController(_mockPortfolioService.Object);
        var user = CreateUserPrincipal(UserData.UsernameB);
        controller.ControllerContext = CreateControllerContext(user);

        // Act
        var result = await controller.GetPortfolios();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(StatusCodes.OK, okResult.StatusCode);
        var returnedPortfolios = Assert.IsType<List<PortfolioDTO>>(okResult.Value);
        Assert.Equal(portfolios.Count, returnedPortfolios.Count);
    }

    [Fact]
    public async Task GetPortfolios_ReturnsNotFound_WhenNoDataExists()
    {
        // Arrange
        _mockPortfolioService.Setup(s => s.GetPortfolioDTOsAsync())
            .ReturnsAsync(new List<PortfolioDTO>());

        var controller = new PortfolioController(_mockPortfolioService.Object);
        var user = CreateUserPrincipal("testuser");
        controller.ControllerContext = CreateControllerContext(user);

        // Act
        var result = await controller.GetPortfolios();

        // Assert
        var notFoundResult = Assert.IsType<NotFoundResult>(result.Result);
        Assert.Equal(StatusCodes.NotFound, notFoundResult.StatusCode);
    }

    [Fact]
    public async Task GetPortfolioBenchmarks_ReturnsOk_WhenDataExists()
    {
        // Arrange
        var benchmarks = new List<PortfolioBenchmarkDTO>
        {
            new PortfolioBenchmarkDTO { PortfolioId = 1, BenchmarkName = "Benchmark 1" },
            new PortfolioBenchmarkDTO { PortfolioId = 2, BenchmarkName = "Benchmark 2" }
        };

        _mockPortfolioService.Setup(s => s.GetPortfolioBenchmarksAsync())
            .ReturnsAsync(benchmarks);

        var controller = new PortfolioController(_mockPortfolioService.Object);
        var user = CreateUserPrincipal("testuser");
        controller.ControllerContext = CreateControllerContext(user);

        // Act
        var result = await controller.GetPortfolioBenchmarks(null);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(StatusCodes.OK, okResult.StatusCode);
        var returnedBenchmarks = Assert.IsType<List<PortfolioBenchmarkDTO>>(okResult.Value);
        Assert.Equal(benchmarks.Count, returnedBenchmarks.Count);
    }

    [Fact]
    public async Task GetPortfolioBenchmarks_ReturnsNotFound_WhenNoDataExists()
    {
        // Arrange
        _mockPortfolioService.Setup(s => s.GetPortfolioBenchmarksAsync())
            .ReturnsAsync(new List<PortfolioBenchmarkDTO>());

        var controller = new PortfolioController(_mockPortfolioService.Object);
        var user = CreateUserPrincipal("testuser");
        controller.ControllerContext = CreateControllerContext(user);

        // Act
        var result = await controller.GetPortfolioBenchmarks(null);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundResult>(result.Result);
        Assert.Equal(StatusCodes.NotFound, notFoundResult.StatusCode);
    }
}