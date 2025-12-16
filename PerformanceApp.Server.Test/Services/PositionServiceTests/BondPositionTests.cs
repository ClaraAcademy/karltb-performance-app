
using Moq;
using PerformanceApp.Data.Builders;
using PerformanceApp.Data.Builders.Defaults;
using PerformanceApp.Server.Test.Services.PositionServiceTests.Fixture;

namespace PerformanceApp.Server.Test.Services.PositionServiceTests;

public class BondPositionTests : PerformanceServiceTestFixture
{
    [Fact]
    public async Task GetBondPositionsAsync_ReturnsMappedBondPositionDTOs()
    {
        // Arrange
        var positions = new BondPositionBuilder().Build();


        _positionRepositoryMock
            .Setup(r => r.GetBondPositionsAsync(It.IsAny<DateOnly>(), It.IsAny<int>()))
            .ReturnsAsync([positions]);

        // Act
        var result = await _positionService.GetBondPositionsAsync(new DateOnly(), 1);

        // Assert
        Assert.Single(result);
        var dto = result.First();
        Assert.Equal(PositionBuilderDefaults.PortfolioId, dto.PortfolioId);
        Assert.Equal(PositionBuilderDefaults.InstrumentId, dto.InstrumentId);
        Assert.Equal(PositionBuilderDefaults.InstrumentNavigation.Name, dto.InstrumentName);
        Assert.Equal(PositionBuilderDefaults.Bankday, dto.Bankday);
        Assert.Equal(PositionValueBuilderDefaults.Value, dto.Value);
        Assert.Equal(InstrumentPriceBuilderDefaults.Price, dto.UnitPrice);
        Assert.Equal(BondPositionBuilderDefaults.Nominal, dto.Nominal);
    }

    [Fact]
    public async Task GetBondPositionsAsync_NoPositions_ReturnsEmptyList()
    {
        // Arrange
        _positionRepositoryMock
            .Setup(r => r.GetBondPositionsAsync(It.IsAny<DateOnly>(), It.IsAny<int>()))
            .ReturnsAsync([]);

        // Act
        var result = await _positionService.GetBondPositionsAsync(new DateOnly(), 1);

        // Assert
        Assert.Empty(result);
    }
}