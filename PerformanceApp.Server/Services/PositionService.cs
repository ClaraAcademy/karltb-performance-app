using PerformanceApp.Data.Dtos;
using PerformanceApp.Data.Helpers;
using PerformanceApp.Data.Mappers;
using PerformanceApp.Data.Models;
using PerformanceApp.Data.Repositories;
using PerformanceApp.Data.Seeding.Dtos;

namespace PerformanceApp.Server.Services
{
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
            return await PositionHelper.GetPositionsAsync(
                _repo.GetStockPositionsAsync,
                PositionMapper.MapToStockPositionDTO,
                bankday,
                portfolioId
            );
        }

        public async Task<List<BondPositionDTO>> GetBondPositionsAsync(DateOnly bankday, int portfolioId)
        {
            return await PositionHelper.GetPositionsAsync(
                _repo.GetBondPositionsAsync,
                PositionMapper.MapToBondPositionDTO,
                bankday,
                portfolioId
            );
        }

        public async Task<List<IndexPositionDTO>> GetIndexPositionsAsync(DateOnly bankday, int portfolioId)
        {
            return await PositionHelper.GetPositionsAsync(
                _repo.GetIndexPositionsAsync,
                PositionMapper.MapToIndexPositionDTO,
                bankday,
                portfolioId
            );
        }
    }
}