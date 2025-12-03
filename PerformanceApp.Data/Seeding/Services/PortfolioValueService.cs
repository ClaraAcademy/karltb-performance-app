using PerformanceApp.Data.Context;
using PerformanceApp.Data.Models;
using PerformanceApp.Data.Repositories;

namespace PerformanceApp.Data.Seeding.Services;

public interface IPortfolioValueService
{
    Task<bool> UpdatePortfolioValuesAsync(DateOnly bankday);
}

public class PortfolioValueService : IPortfolioValueService
{
    private readonly IPortfolioValueRepository _portfolioValueRepository;
    private readonly IDateInfoService _dateInfoService;
    private readonly IPositionRepository _positionRepository;
    private readonly IPositionValueRepository _positionValueRepository;

    public PortfolioValueService(PadbContext context)
    {
        _portfolioValueRepository = new PortfolioValueRepository(context);
        _dateInfoService = new DateInfoService(context);
        _positionRepository = new PositionRepository(context);
        _positionValueRepository = new PositionValueRepository(context);
    }

    private record Key(int PortfolioId, int InstrumentId);

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
            .GroupBy(pv => pv.PositionId)
            .Join(filteredPositions, GetKey, GetKey, (g, p) => MapToPortfolioValue(g, p, bankday))
            .ToList();

        await _portfolioValueRepository.AddPortfolioValuesAsync(portfolioValues);

        return true;
    }
}