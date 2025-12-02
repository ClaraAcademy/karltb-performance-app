using PerformanceApp.Data.Models;
using PerformanceApp.Data.Repositories;

namespace PerformanceApp.Data.Seeding.Services;

public interface IPositionValueService
{
    Task<bool> UpdatePositionValuesAsync(DateOnly bankday);
}

public class PositionValueService : IPositionValueService
{
    private readonly IPositionValueRepository _positionValueRepository;
    private readonly IInstrumentPriceRepository _instrumentPriceRepository;
    private readonly IPositionRepository _positionRepository;
    private readonly IPortfolioRepository _portfolioRepository;
    private readonly IDateInfoService _dateInfoService;

    public PositionValueService(
        IPositionValueRepository positionValueRepository,
        IInstrumentPriceRepository instrumentPriceRepository,
        IPositionRepository positionRepository,
        IPortfolioRepository portfolioRepository,
        IDateInfoService dateInfoService
    )
    {
        _positionValueRepository = positionValueRepository;
        _instrumentPriceRepository = instrumentPriceRepository;
        _positionRepository = positionRepository;
        _portfolioRepository = portfolioRepository;
        _dateInfoService = dateInfoService;
    }

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
        var candidates = new[] { position.Count, position.Amount, position.Nominal, position.Proportion };
        var value = candidates.FirstOrDefault(c => c.HasValue)
            ?? throw new InvalidOperationException("Position has no weight defined.");
        return value;
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

        var positions = await _positionRepository.GetPositionsAsync();
        var currentPositions = positions
            .Where(p => p.Bankday == bankday)
            .ToList();

        var instrumentPrices = await _instrumentPriceRepository.GetInstrumentPricesAsync();
        var currentPrices = instrumentPrices
            .Where(ip => ip.Bankday == bankday)
            .ToList();

        var portfolios = await _portfolioRepository.GetProperPortfoliosAsync();
        var portfolioIds = portfolios.Select(p => p.Id).OfType<int>().ToHashSet();

        var positionValues = currentPositions
            .Select(p => MapToDto(p, bankday))
            .Where(dto => portfolioIds.Contains(dto.PortfolioId)) // Only consider positions in proper portfolios
            .Join(currentPrices, GetKey, GetKey, MapToPositionValue)
            .ToList();

        await _positionValueRepository.AddPositionValuesAsync(positionValues);
        return true;
    }
}