using PerformanceApp.Data.Models;
using PerformanceApp.Data.Repositories;

namespace PerformanceApp.Data.Test.Repositories;

public class PositionRepositoryTest
{
    [Fact]
    public async Task GetPositionsAsync_ReturnsAllPositions()
    {
        // Arrange
        var context = RepositoryTest.GetContext();
        context.Positions.AddRange(
            new Position { Id = 1, PortfolioId = 1, Bankday = DateOnly.FromDateTime(DateTime.Now) },
            new Position { Id = 2, PortfolioId = 2, Bankday = DateOnly.FromDateTime(DateTime.Now) }
        );
        await context.SaveChangesAsync();

        var repository = new PositionRepository(context);

        // Act
        var result = await repository.GetPositionsAsync();

        // Assert
        Assert.NotNull(result);
        var positions = result.ToList();
        Assert.Equal(2, positions.Count);
        Assert.Contains(positions, p => p.Id == 1 && p.PortfolioId == 1);
        Assert.Contains(positions, p => p.Id == 2 && p.PortfolioId == 2);
    }

    [Fact]
    public async Task GetPositionsAsync_ReturnsEmptyListWhenNoData()
    {
        // Arrange
        var context = RepositoryTest.GetContext();
        var repository = new PositionRepository(context);

        // Act
        var result = await repository.GetPositionsAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetStockPositionsAsync_ReturnsFilteredPositions()
    {
        // Arrange
        var context = RepositoryTest.GetContext();
        var bankday = DateOnly.FromDateTime(DateTime.Now);
        context.Positions.AddRange(
            new Position
            {
                Id = 1,
                PortfolioId = 1,
                Bankday = bankday,
                InstrumentNavigation = new Instrument
                {
                    InstrumentTypeNavigation = new InstrumentType { Name = "Stock" }
                }
            },
            new Position
            {
                Id = 2,
                PortfolioId = 1,
                Bankday = bankday,
                InstrumentNavigation = new Instrument
                {
                    InstrumentTypeNavigation = new InstrumentType { Name = "Bond" }
                }
            }
        );
        await context.SaveChangesAsync();

        var repository = new PositionRepository(context);

        // Act
        var result = await repository.GetStockPositionsAsync(bankday, 1);

        // Assert
        Assert.NotNull(result);
        var positions = result.ToList();
        Assert.Single(positions);
        Assert.Equal(1, positions[0].Id);
    }

    [Fact]
    public async Task GetStockPositionsAsync_ReturnsEmptyListOnInvalidBankday()
    {
        // Arrange
        var context = RepositoryTest.GetContext();
        var bankday = DateOnly.FromDateTime(DateTime.Now);
        context.Positions.Add(
            new Position
            {
                Id = 1,
                PortfolioId = 1,
                Bankday = bankday,
                InstrumentNavigation = new Instrument
                {
                    InstrumentTypeNavigation = new InstrumentType { Name = "Stock" }
                }
            }
        );
        await context.SaveChangesAsync();

        var repository = new PositionRepository(context);

        // Act
        var result = await repository.GetStockPositionsAsync(bankday.AddDays(1), 1);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetStockPositionsAsync_ReturnsEmptyListOnInvalidPortfolioId()
    {
        // Arrange
        var context = RepositoryTest.GetContext();
        var bankday = DateOnly.FromDateTime(DateTime.Now);
        context.Positions.Add(
            new Position
            {
                Id = 1,
                PortfolioId = 1,
                Bankday = bankday,
                InstrumentNavigation = new Instrument
                {
                    InstrumentTypeNavigation = new InstrumentType { Name = "Stock" }
                }
            }
        );
        await context.SaveChangesAsync();

        var repository = new PositionRepository(context);

        // Act
        var result = await repository.GetStockPositionsAsync(bankday, 2);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetStockPositionsAsync_ReturnsEmptyListWhenNoData()
    {
        // Arrange
        var context = RepositoryTest.GetContext();
        var repository = new PositionRepository(context);
        var bankday = DateOnly.FromDateTime(DateTime.Now);

        // Act
        var result = await repository.GetStockPositionsAsync(bankday, 1);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetBondPositionsAsync_ReturnsFilteredPositions()
    {
        // Arrange
        var context = RepositoryTest.GetContext();
        var bankday = DateOnly.FromDateTime(DateTime.Now);
        context.Positions.AddRange(
            new Position
            {
                Id = 1,
                PortfolioId = 1,
                Bankday = bankday,
                InstrumentNavigation = new Instrument
                {
                    InstrumentTypeNavigation = new InstrumentType { Name = "Bond" }
                }
            },
            new Position
            {
                Id = 2,
                PortfolioId = 1,
                Bankday = bankday,
                InstrumentNavigation = new Instrument
                {
                    InstrumentTypeNavigation = new InstrumentType { Name = "Stock" }
                }
            }
        );
        await context.SaveChangesAsync();

        var repository = new PositionRepository(context);

        // Act
        var result = await repository.GetBondPositionsAsync(bankday, 1);

        // Assert
        Assert.NotNull(result);
        var positions = result.ToList();
        Assert.Single(positions);
        Assert.Equal(1, positions[0].Id);
    }

    [Fact]
    public async Task GetBondPositionsAsync_ReturnsEmptyListOnInvalidBankday()
    {
        // Arrange
        var context = RepositoryTest.GetContext();
        var bankday = DateOnly.FromDateTime(DateTime.Now);
        context.Positions.Add(
            new Position
            {
                Id = 1,
                PortfolioId = 1,
                Bankday = bankday,
                InstrumentNavigation = new Instrument
                {
                    InstrumentTypeNavigation = new InstrumentType { Name = "Bond" }
                }
            }
        );
        await context.SaveChangesAsync();

        var repository = new PositionRepository(context);

        // Act
        var result = await repository.GetBondPositionsAsync(bankday.AddDays(1), 1);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetBondPositionsAsync_ReturnsEmptyListOnInvalidPortfolioId()
    {
        // Arrange
        var context = RepositoryTest.GetContext();
        var bankday = DateOnly.FromDateTime(DateTime.Now);
        context.Positions.Add(
            new Position
            {
                Id = 1,
                PortfolioId = 1,
                Bankday = bankday,
                InstrumentNavigation = new Instrument
                {
                    InstrumentTypeNavigation = new InstrumentType { Name = "Bond" }
                }
            }
        );
        await context.SaveChangesAsync();

        var repository = new PositionRepository(context);

        // Act
        var result = await repository.GetBondPositionsAsync(bankday, 2);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetBondPositionsAsync_ReturnsEmptyListWhenNoData()
    {
        // Arrange
        var context = RepositoryTest.GetContext();
        var repository = new PositionRepository(context);
        var bankday = DateOnly.FromDateTime(DateTime.Now);

        // Act
        var result = await repository.GetBondPositionsAsync(bankday, 1);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetIndexPositionsAsync_ReturnsFilteredPositions()
    {
        // Arrange
        var context = RepositoryTest.GetContext();
        var bankday = DateOnly.FromDateTime(DateTime.Now);
        context.Positions.AddRange(
            new Position
            {
                Id = 1,
                PortfolioId = 1,
                Bankday = bankday,
                InstrumentNavigation = new Instrument
                {
                    InstrumentTypeNavigation = new InstrumentType { Name = "Index" }
                }
            },
            new Position
            {
                Id = 2,
                PortfolioId = 1,
                Bankday = bankday,
                InstrumentNavigation = new Instrument
                {
                    InstrumentTypeNavigation = new InstrumentType { Name = "Stock" }
                }
            }
        );
        await context.SaveChangesAsync();

        var repository = new PositionRepository(context);

        // Act
        var result = await repository.GetIndexPositionsAsync(bankday, 1);

        // Assert
        Assert.NotNull(result);
        var positions = result.ToList();
        Assert.Single(positions);
        Assert.Equal(1, positions[0].Id);
    }

    [Fact]
    public async Task GetIndexPositionsAsync_ReturnsEmptyListOnInvalidBankday()
    {
        // Arrange
        var context = RepositoryTest.GetContext();
        var bankday = DateOnly.FromDateTime(DateTime.Now);
        context.Positions.Add(
            new Position
            {
                Id = 1,
                PortfolioId = 1,
                Bankday = bankday,
                InstrumentNavigation = new Instrument
                {
                    InstrumentTypeNavigation = new InstrumentType { Name = "Index" }
                }
            }
        );
        await context.SaveChangesAsync();

        var repository = new PositionRepository(context);

        // Act
        var result = await repository.GetIndexPositionsAsync(bankday.AddDays(1), 1);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetIndexPositionsAsync_ReturnsEmptyListOnInvalidPortfolioId()
    {
        // Arrange
        var context = RepositoryTest.GetContext();
        var bankday = DateOnly.FromDateTime(DateTime.Now);
        context.Positions.Add(
            new Position
            {
                Id = 1,
                PortfolioId = 1,
                Bankday = bankday,
                InstrumentNavigation = new Instrument
                {
                    InstrumentTypeNavigation = new InstrumentType { Name = "Index" }
                }
            }
        );
        await context.SaveChangesAsync();

        var repository = new PositionRepository(context);

        // Act
        var result = await repository.GetIndexPositionsAsync(bankday, 2);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetIndexPositionsAsync_ReturnsEmptyListWhenNoData()
    {
        // Arrange
        var context = RepositoryTest.GetContext();
        var repository = new PositionRepository(context);
        var bankday = DateOnly.FromDateTime(DateTime.Now);

        // Act
        var result = await repository.GetIndexPositionsAsync(bankday, 1);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

}