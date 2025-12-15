using PerformanceApp.Data.Context;
using PerformanceApp.Data.Models;
using PerformanceApp.Data.Repositories;

namespace PerformanceApp.Seeder.Services;

public interface IPositionValueService
{
    Task<bool> UpdatePositionValuesAsync(DateOnly bankday);
}

public class PositionValueService(PadbContext context) : IPositionValueService
{
    private readonly IPositionValueRepository _positionValueRepository = new PositionValueRepository(context);
    private readonly IInstrumentPriceRepository _instrumentPriceRepository = new InstrumentPriceRepository(context);
    private readonly IPositionRepository _positionRepository = new PositionRepository(context);
    private readonly IPortfolioRepository _portfolioRepository = new PortfolioRepository(context);
    private readonly IDateInfoService _dateInfoService = new DateInfoService(context);

    private record Dto(
        int PositionId,
        int PortfolioId,
        int InstrumentId,
        DateOnly Bankday,
        decimal Weight
    );
    private record Key(int PositionId, DateOnly Bankday);
    private static decimal GetWeight(Position position)
    {
        var candidates = new[] {
            position.Count,
            position.Amount,
            position.Nominal,
            position.Proportion
        };

        var weight = candidates
            .Select(v => v ?? 0)
            .FirstOrDefault(v => v != 0);

        return weight;
    }

    private static Dto MapToDto(Position position, DateOnly bankday)
    {
        return new Dto(
            position.Id,
            position.PortfolioId ?? throw new InvalidOperationException("Position has no PortfolioId."),
            position.InstrumentId ?? throw new InvalidOperationException("Position has no InstrumentId."),
            bankday,
            GetWeight(position)
        );
    }
    private static PositionValue MapToPositionValue(Dto dto, InstrumentPrice ip)
    {
        return new PositionValue
        {
            PositionId = dto.PositionId,
            Bankday = dto.Bankday,
            Value = dto.Weight * ip.Price
        };
    }
    private static int GetKey(Dto dto) => dto.InstrumentId;
    private static int GetKey(InstrumentPrice ip) => ip.InstrumentId;

    public async Task<bool> UpdatePositionValuesAsync(DateOnly bankday)
    {
        var bankdayExists = await _dateInfoService.BankdayExistsAsync(bankday);

        if (!bankdayExists)
        {
            return false;
        }
        var portfolios = (await _portfolioRepository.GetProperPortfoliosAsync()).ToList();
        var portfolioIds = portfolios.Select(p => p.Id).OfType<int>().ToHashSet();

        var positions = await _positionRepository.GetPositionsAsync();
        var currentPositions = positions
            .Where(p => p.Bankday == bankday)
            .Select(p => MapToDto(p, bankday))
            .Where(dto => portfolioIds.Contains(dto.PortfolioId)) // Only consider positions in proper portfolios
            .ToList();

        var instrumentPrices = await _instrumentPriceRepository.GetInstrumentPricesAsync();
        var currentPrices = instrumentPrices
            .Where(ip => ip.Bankday == bankday)
            .ToList();


        var positionValues = currentPositions
            .Join(currentPrices, GetKey, GetKey, MapToPositionValue)
            .Where(dto => dto.Value != 0) // Exclude zero values
            .ToList();

        await _positionValueRepository.AddPositionValuesAsync(positionValues);
        return true;
    }
}