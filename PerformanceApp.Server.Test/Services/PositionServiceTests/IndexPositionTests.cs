
using Moq;
using PerformanceApp.Data.Builders;
using PerformanceApp.Data.Builders.Defaults;
using PerformanceApp.Data.Models;
using PerformanceApp.Server.Test.Services.PositionServiceTests.Fixture;

namespace PerformanceApp.Server.Test.Services.PositionServiceTests;

public class IndexPositionTests : PerformanceServiceTestFixture
{
    [Fact]
    public async Task GetIndexPositionsAsync_ReturnsMappedIndexPositionDTOs()
    {
        // Arrange
        var positions = new IndexPositionBuilder().Build();

        _positionRepositoryMock
            .Setup(r => r.GetIndexPositionsAsync(It.IsAny<DateOnly>(), It.IsAny<int>()))
            .ReturnsAsync([positions]);
        // Act
        var result = await _positionService.GetIndexPositionsAsync(new DateOnly(), 1);

        // Assert
        Assert.Single(result);
        var dto = result[0];
        Assert.Equal(PositionBuilderDefaults.PortfolioId, dto.PortfolioId);
        Assert.Equal(PositionBuilderDefaults.InstrumentId, dto.InstrumentId);
        Assert.Equal(PositionBuilderDefaults.InstrumentNavigation.Name, dto.InstrumentName);
        Assert.Equal(PositionBuilderDefaults.Bankday, dto.Bankday);
        Assert.Equal(PositionValueBuilderDefaults.Value, dto.Value);
        Assert.Equal(InstrumentPriceBuilderDefaults.Price, dto.UnitPrice);
        Assert.Equal(IndexPositionBuilderDefaults.Proportion, dto.Proportion);
    }

    [Fact]
    public async Task GetIndexPositionsAsync_NoPositions_ReturnsEmptyList()
    {
        // Arrange
        _positionRepositoryMock
            .Setup(r => r.GetIndexPositionsAsync(It.IsAny<DateOnly>(), It.IsAny<int>()))
            .ReturnsAsync([]);

        // Act
        var result = await _positionService.GetIndexPositionsAsync(new DateOnly(), 1);

        // Assert
        Assert.Empty(result);
    }
}