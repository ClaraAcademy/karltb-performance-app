using PerformanceApp.Data.Models;
using PerformanceApp.Infrastructure.Repositories;
namespace PerformanceApp.Data.Test.Repositories;

public class KeyFigureValueRepositoryTest : BaseRepositoryTest
{
    private readonly KeyFigureValueRepository _repository;

    public KeyFigureValueRepositoryTest()
    {
        _repository = new KeyFigureValueRepository(_context);
    }

    private static Portfolio CreatePortfolio(int i) => new Portfolio { Id = i, Name = $"Portfolio {i}" };
    private static List<Portfolio> CreatePortfolios(int count)
    {
        return Enumerable.Range(1, count)
            .Select(CreatePortfolio)
            .ToList();
    }
    private static KeyFigureValue CreateKeyFigureValue(int keyFigureId, int portfolioId)
    {
        return new KeyFigureValue
        {
            KeyFigureId = keyFigureId,
            PortfolioId = portfolioId,
            Value = keyFigureId * portfolioId * 100m
        };
    }
    private static List<KeyFigureValue> CreateKeyFigureValues(int nKeyFigures, int nPortfolios)
    {
        var result = new List<KeyFigureValue>();
        for (int pfId = 1; pfId <= nPortfolios; pfId++)
        {
            for (int kfId = 1; kfId <= nKeyFigures; kfId++)
            {
                result.Add(CreateKeyFigureValue(kfId, pfId));
            }
        }
        return result;
    }

    [Fact]
    public async Task GetKeyFigureValuesAsync_ReturnsValues_ForGivenPortfolioId()
    {
        // Arrange
        var nPortfolios = 2;
        var portfolios = CreatePortfolios(nPortfolios);
        _context.Portfolios.AddRange(portfolios);
        await _context.SaveChangesAsync();

        var nKeyFigures = 4;
        var keyFigureValues = CreateKeyFigureValues(nKeyFigures, nPortfolios);
        _context.KeyFigureValues.AddRange(keyFigureValues);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.GetKeyFigureValuesAsync(1);

        // Assert
        Assert.Equal(nKeyFigures, result.Count());
    }

    [Fact]
    public async Task GetKeyFigureValuesAsync_ReturnsEmpty_ForNonExistingPortfolioId()
    {
        // Act
        var result = await _repository.GetKeyFigureValuesAsync(999);

        // Assert
        Assert.Empty(result);
    }
}