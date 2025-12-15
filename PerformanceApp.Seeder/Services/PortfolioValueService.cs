using PerformanceApp.Infrastructure.Context;
using PerformanceApp.Data.Models;
using PerformanceApp.Infrastructure.Repositories;

namespace PerformanceApp.Seeder.Services;

public interface IPortfolioValueService
{
    Task<bool> UpdatePortfolioValuesAsync(DateOnly bankday);
}

public class PortfolioValueService(PadbContext context) : IPortfolioValueService
{
    private readonly IPortfolioValueRepository _portfolioValueRepository = new PortfolioValueRepository(context);
    private readonly IDateInfoService _dateInfoService = new DateInfoService(context);
    private readonly IPositionRepository _positionRepository = new PositionRepository(context);
    private readonly IPositionValueRepository _positionValueRepository = new PositionValueRepository(context);

    private record Key(int PortfolioId, int InstrumentId);

    private static int GetPortfolioId(PositionValue pv) => pv.PositionNavigation.PortfolioId!.Value;
    private static int GetKey(IGrouping<int, PositionValue> g) => g.Key;
    private static int GetKey(Position p) => p.Id;
    private static PortfolioValue MapToPortfolioValue(IGrouping<int, PositionValue> g, Position p, DateOnly bankday)
    {
        return new PortfolioValue
        {
            PortfolioId = p.PortfolioId!.Value,
            Bankday = bankday,
            Value = g.Sum(v => v.Value)
        };
    }

    public async Task<bool> UpdatePortfolioValuesAsync(DateOnly bankday)
    {
        var bankdayExists = await _dateInfoService.BankdayExistsAsync(bankday);
        if (!bankdayExists)
        {
            return false;
        }

        var positions = await _positionRepository.GetPositionsAsync();
        var filteredPositions = positions.Where(p => p.PortfolioId.HasValue).ToList();

        var positionValues = await _positionValueRepository.GetPositionValuesAsync();
        var currentPositionValues = positionValues
            .Where(pv => pv.Bankday == bankday)
            .ToList();

        var portfolioValues = currentPositionValues
            .GroupBy(GetPortfolioId)
            .Join(filteredPositions, GetKey, GetKey, (g, p) => MapToPortfolioValue(g, p, bankday))
            .ToList();

        await _portfolioValueRepository.AddPortfolioValuesAsync(portfolioValues);

        return true;
    }
}