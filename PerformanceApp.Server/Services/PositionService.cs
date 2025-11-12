using System.Security.Cryptography;
using PerformanceApp.Data.Models;
using PerformanceApp.Data.Repositories;
using PerformanceApp.Server.DTOs;

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

        private static decimal? GetPositionValue(Position p)
            => p.PositionValuesNavigation?.SingleOrDefault(pv => pv.Bankday == p.Bankday)?.PositionValue1;

        private static decimal? GetInstrumentUnitPrice(Position p)
            => p.InstrumentNavigation?.InstrumentPricesNavigation?.SingleOrDefault(ip => ip.Bankday == p.Bankday)?.Price;

        private static string? GetInstrumentName(Position p)
            => p.InstrumentNavigation?.InstrumentName;

        private static TBase MapCommonFields<TBase>(Position p, TBase dto)
            where TBase : PositionDTO
        {
            dto.PortfolioId = p.PortfolioId;
            dto.InstrumentId = p.InstrumentId;
            dto.InstrumentName = GetInstrumentName(p);
            dto.Bankday = p.Bankday;
            dto.Value = GetPositionValue(p);
            dto.UnitPrice = GetInstrumentUnitPrice(p);
            return dto;
        }

        private StockPositionDTO StockPositionSelector(Position p)
            => MapCommonFields(p, new StockPositionDTO { Count = p.Count });

        private BondPositionDTO BondPositionSelector(Position p)
            => MapCommonFields(p, new BondPositionDTO { Nominal = p.Nominal });

        private IndexPositionDTO IndexPositionSelector(Position p)
            => MapCommonFields(p, new IndexPositionDTO { Proportion = p.Proportion });

        
        public async Task<List<StockPositionDTO>> GetStockPositionsAsync(DateOnly bankday, int portfolioId)
            => (await _repo.GetStockPositionsAsync(bankday, portfolioId))
                .Select(StockPositionSelector)
                .ToList();

        public async Task<List<BondPositionDTO>> GetBondPositionsAsync(DateOnly bankday, int portfolioId)
            => (await _repo.GetBondPositionsAsync(bankday, portfolioId))
                .Select(BondPositionSelector)
                .ToList();
        public async Task<List<IndexPositionDTO>> GetIndexPositionsAsync(DateOnly bankday, int portfolioId)
            => (await _repo.GetIndexPositionsAsync(bankday, portfolioId))
                .Select(IndexPositionSelector)
                .ToList();
    }
}