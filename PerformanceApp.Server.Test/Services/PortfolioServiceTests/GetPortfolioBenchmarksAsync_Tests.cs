using Moq;
using PerformanceApp.Data.Models;
using PerformanceApp.Data.Builders;
using PerformanceApp.Server.Test.Services.PortfolioServiceTests.Fixture;
using Xunit.Sdk;
using PerformanceApp.Data.Builders.Defaults;

namespace PerformanceApp.Server.Test.Services.PortfolioServiceTests;

public class GetPortfolioBenchmarksAsync_Tests() : PortfolioServiceTestFixture()
{
    [Fact]
    public async Task GetPortfolioBenchmarksAsync_ReturnsExpectedResults()
    {
        // Arrange
        var benchmark = PortfolioBuilderDefaults.Benchmark;
        var portfolio = new PortfolioBuilder()
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
        Assert.Equal(benchmark.Id, actual.BenchmarkId);
        Assert.Equal(benchmark.Name, actual.BenchmarkName);
        Assert.Equal(portfolio.Id, actual.PortfolioId);
        Assert.Equal(portfolio.Name, actual.PortfolioName);
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
        var benchmark = PortfolioBuilderDefaults.Benchmark;
        var portfolio = new PortfolioBuilder()
            .WithBenchmark(benchmark)
            .Build();

        _portfolioRepositoryMock
            .Setup(r => r.GetPortfolioAsync(It.IsAny<int>()))
            .ReturnsAsync(portfolio);

        // Act
        var result = await _portfolioService.GetPortfolioBenchmarksAsync(1);
        var actual = result.Single();

        // Assert
        Assert.Single(result);
        Assert.Equal(benchmark.Id, actual.BenchmarkId);
        Assert.Equal(benchmark.Name, actual.BenchmarkName);
        Assert.Equal(portfolio.Id, actual.PortfolioId);
        Assert.Equal(portfolio.Name, actual.PortfolioName);
    }

    [Fact]
    public async Task GetPortfolioBenchmarksAsync_WithUserId_ReturnsExpectedResults()
    {
        // Arrange
        var benchmark = PortfolioBuilderDefaults.Benchmark;
        var portfolio = new PortfolioBuilder()
            .WithBenchmark(benchmark)
            .Build();

        _portfolioRepositoryMock
            .Setup(r => r.GetPortfoliosAsync(It.IsAny<string>()))
            .ReturnsAsync([portfolio]);

        // Act
        var result = await _portfolioService.GetPortfolioBenchmarksAsync("some-user-id");
        var actual = result.Single();

        // Assert
        Assert.Single(result);
        Assert.Equal(benchmark.Id, actual.BenchmarkId);
        Assert.Equal(benchmark.Name, actual.BenchmarkName);
        Assert.Equal(portfolio.Id, actual.PortfolioId);
        Assert.Equal(portfolio.Name, actual.PortfolioName);
    }

    [Fact]
    public async Task GetPortfolioBenchmarksAsync_WithPortfolioId_ReturnsEmptyList_WhenNoBenchmarks()
    {
        // Arrange
        var portfolio = new PortfolioBuilder()
            .WithBenchmarks([])
            .Build();

        _portfolioRepositoryMock
            .Setup(r => r.GetPortfolioAsync(It.IsAny<int>()))
            .ReturnsAsync(portfolio);

        // Act
        var result = await _portfolioService.GetPortfolioBenchmarksAsync(-1);

        // Assert
        Assert.Empty(result);
    }
}