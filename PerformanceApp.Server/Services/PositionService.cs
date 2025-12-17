using PerformanceApp.Data.Dtos;
using PerformanceApp.Data.Helpers;
using PerformanceApp.Data.Mappers;
using PerformanceApp.Infrastructure.Repositories;

namespace PerformanceApp.Server.Services;

public interface IPositionService
{
    Task<List<StockPositionDTO>> GetStockPositionsAsync(DateOnly bankday, int portfolioId);
    Task<List<BondPositionDTO>> GetBondPositionsAsync(DateOnly bankday, int portfolioId);
    Task<List<IndexPositionDTO>> GetIndexPositionsAsync(DateOnly bankday, int portfolioId);
}

public class PositionService(IPositionRepository repo) : IPositionService
{
    private readonly IPositionRepository _repo = repo;

    public async Task<List<StockPositionDTO>> GetStockPositionsAsync(DateOnly bankday, int portfolioId)
    {
        var positions = await _repo.GetStockPositionsAsync(bankday, portfolioId);

        return positions
            .Select(PositionMapper.MapToStockPositionDTO)
            .ToList();
    }

    public async Task<List<BondPositionDTO>> GetBondPositionsAsync(DateOnly bankday, int portfolioId)
    {
        var positions = await _repo.GetBondPositionsAsync(bankday, portfolioId);

        return positions
            .Select(PositionMapper.MapToBondPositionDTO)
            .ToList();
    }

    public async Task<List<IndexPositionDTO>> GetIndexPositionsAsync(DateOnly bankday, int portfolioId)
    {
        var positions = await _repo.GetIndexPositionsAsync(bankday, portfolioId);

        return positions
            .Select(PositionMapper.MapToIndexPositionDTO)
            .ToList();
    }
}