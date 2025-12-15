using Moq;
using PerformanceApp.Data.Models;
using PerformanceApp.Data.Builders;
using PerformanceApp.Server.Test.Services.PortfolioServiceTests.Fixture;
using Xunit.Sdk;

namespace PerformanceApp.Server.Test.Services.PortfolioServiceTests;

public class GetPortfolioBenchmarksAsync_Tests() : PortfolioServiceTestFixture()
{
    [Fact]
    public async Task GetPortfolioBenchmarksAsync_ReturnsExpectedResults()
    {
        // Arrange
        var benchmarkId = 1000;
        var portfolioId = 2000;
        var benchmarkName = "Benchmark Portfolio";
        var portfolioName = "Main Portfolio";

        var benchmark = new PortfolioBuilder()
            .WithId(benchmarkId)
            .WithName(benchmarkName)
            .Build();
        var portfolio = new PortfolioBuilder()
            .WithId(portfolioId)
            .WithName(portfolioName)
            .WithBenchmarks([benchmark])
            .Build();

        _portfolioRepositoryMock
            .Setup(r => r.GetProperPortfoliosAsync())
            .ReturnsAsync([portfolio]);

        // Act
        var result = await _portfolioService.GetPortfolioBenchmarksAsync();
        var actual = result.Single();

        // Assert
        Assert.Single(result);
        Assert.Equal(benchmarkId, actual.BenchmarkId);
        Assert.Equal(benchmarkName, actual.BenchmarkName);
        Assert.Equal(portfolioId, actual.PortfolioId);
        Assert.Equal(portfolioName, actual.PortfolioName);
    }

    [Fact]
    public async Task GetPortfolioBenchmarksAsync_ReturnsEmptyList_WhenNoBenchmarks()
    {
        // Arrange
        _portfolioRepositoryMock
            .Setup(r => r.GetProperPortfoliosAsync())
            .ReturnsAsync([]);

        // Act
        var result = await _portfolioService.GetPortfolioBenchmarksAsync();

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetPortfolioBenchmarksAsync_WithPortfolioId_ReturnsExpectedResults()
    {
        // Arrange
        var portfolioId = 1;
        var benchmarkId = 100;
        var portfolioName = "Portfolio 1";
        var benchmarkName = "Benchmark 1";

        var benchmark = new PortfolioBuilder()
            .WithId(benchmarkId)
            .WithName(benchmarkName)
            .Build();

        var portfolio = new PortfolioBuilder()
            .WithId(portfolioId)
            .WithName(portfolioName)
            .WithBenchmark(benchmark)
            .Build();

        _portfolioRepositoryMock
            .Setup(r => r.GetPortfolioAsync(It.IsAny<int>()))
            .ReturnsAsync(portfolio);

        // Act
        var result = await _portfolioService.GetPortfolioBenchmarksAsync(portfolioId);
        var actual = result.Single();

        // Assert
        Assert.Single(result);
        Assert.Equal(benchmarkId, actual.BenchmarkId);
        Assert.Equal(benchmarkName, actual.BenchmarkName);
        Assert.Equal(portfolioId, actual.PortfolioId);
        Assert.Equal(portfolioName, actual.PortfolioName);
    }

    [Fact]
    public async Task GetPortfolioBenchmarksAsync_WithUserId_ReturnsExpectedResults()
    {
        // Arrange
        var portfolioId = 1;
        var benchmarkId = 100;
        var portfolioName = "Portfolio 1";
        var benchmarkName = "Benchmark 1";
        var userId = "user-123";

        var benchmark = new PortfolioBuilder()
            .WithId(benchmarkId)
            .WithName(benchmarkName)
            .Build();

        var portfolio = new PortfolioBuilder()
            .WithId(portfolioId)
            .WithName(portfolioName)
            .WithBenchmark(benchmark)
            .Build();

        _portfolioRepositoryMock
            .Setup(r => r.GetPortfoliosAsync(It.IsAny<string>()))
            .ReturnsAsync([portfolio]);

        // Act
        var result = await _portfolioService.GetPortfolioBenchmarksAsync(userId);
        var actual = result.Single();

        // Assert
        Assert.Single(result);
        Assert.Equal(benchmarkId, actual.BenchmarkId);
        Assert.Equal(benchmarkName, actual.BenchmarkName);
        Assert.Equal(portfolioId, actual.PortfolioId);
        Assert.Equal(portfolioName, actual.PortfolioName);
    }

    [Fact]
    public async Task GetPortfolioBenchmarksAsync_WithPortfolioId_ReturnsEmptyList_WhenNoBenchmarks()
    {
        // Arrange
        var portfolioId = 1;
        var portfolioName = "Portfolio 1";

        var portfolio = new PortfolioBuilder()
            .WithId(portfolioId)
            .WithName(portfolioName)
            .WithBenchmarks([])
            .Build();

        _portfolioRepositoryMock
            .Setup(r => r.GetPortfolioAsync(portfolioId))
            .ReturnsAsync(portfolio);

        // Act
        var result = await _portfolioService.GetPortfolioBenchmarksAsync(portfolioId);

        // Assert
        Assert.Empty(result);
    }
}