using PerformanceApp.Server.Dtos;
using PerformanceApp.Data.Models;
using PerformanceApp.Data.Repositories;
namespace PerformanceApp.Server.Services
{
    public interface IPerformanceService
    {
        Task<List<PortfolioBenchmarkKeyFigureDTO>> GetPortfolioBenchmarkKeyFigureValues(int portfolioId);
    }

    public class PerformanceService(IKeyFigureValueRepository keyFigureValueRepository, IPortfolioService portfolioService) : IPerformanceService
    {
        private readonly IKeyFigureValueRepository KeyFigureValueRepository = keyFigureValueRepository;
        private readonly IPortfolioService PortfolioService = portfolioService;

        private static PortfolioBenchmarkKeyFigureDTO MapInitial(KeyFigureInfo kfi, PortfolioBenchmarkDTO pb)
            => new PortfolioBenchmarkKeyFigureDTO
            {
                KeyFigureId = kfi.Id,
                KeyFigureName = kfi.Name,
                PortfolioId = pb.PortfolioId,
                PortfolioName = pb.PortfolioName,
                PortfolioValue = null,
                BenchmarkId = pb.BenchmarkId,
                BenchmarkName = pb.BenchmarkName,
                BenchmarkValue = null
            };

        private static PortfolioBenchmarkKeyFigureDTO MapFinal(PortfolioBenchmarkKeyFigureDTO c, KeyFigureValue pv, KeyFigureValue bv)
                => new PortfolioBenchmarkKeyFigureDTO
                {
                    KeyFigureId = c.KeyFigureId,
                    KeyFigureName = c.KeyFigureName,
                    PortfolioId = c.PortfolioId,
                    PortfolioName = c.PortfolioName,
                    PortfolioValue = pv?.Value,
                    BenchmarkId = c.BenchmarkId,
                    BenchmarkName = c.BenchmarkName,
                    BenchmarkValue = bv?.Value
                };

        private record Key(int PortfolioId, int KeyFigureId);

        private static Key GetPortfolioKey(PortfolioBenchmarkKeyFigureDTO pb) => new(pb.PortfolioId, pb.KeyFigureId);
        private static Key GetBenchmarkKey(PortfolioBenchmarkKeyFigureDTO pb) => new(pb.BenchmarkId, pb.KeyFigureId);
        private static Key GetPortfolioKey(KeyFigureValue v) => new(v.PortfolioId, v.KeyFigureId);
        private static Key GetBenchmarkKey(KeyFigureValue v) => new(v.PortfolioId, v.KeyFigureId);


        public async Task<List<PortfolioBenchmarkKeyFigureDTO>> GetPortfolioBenchmarkKeyFigureValues(int portfolioId)
        {
            var dtos = await PortfolioService.GetPortfolioBenchmarksAsync(portfolioId);

            if (dtos.Count != 1)
            {
                return [];
            }

            var dto = dtos.Single();

            var benchmarkId = dto.BenchmarkId;
            var keyFigureInfos = await KeyFigureValueRepository.GetKeyFigureInfosAsync();

            if (!keyFigureInfos.Any())
            {
                return [];
            }

            var portfolioKeyFigureValues = await KeyFigureValueRepository.GetKeyFigureValuesAsync(portfolioId);
            var benchmarkKeyFigureValues = await KeyFigureValueRepository.GetKeyFigureValuesAsync(benchmarkId);

            var empty = !portfolioKeyFigureValues.Any() && !benchmarkKeyFigureValues.Any();

            if (empty)
            {
                return [];
            }

            var combinations = keyFigureInfos.Select(kfi => MapInitial(kfi, dto)).ToList();

            return (
                from c in combinations

                join pv in portfolioKeyFigureValues on GetPortfolioKey(c) equals GetPortfolioKey(pv) into psg
                from pv in psg.DefaultIfEmpty() // Left Join portfolio values

                join bv in benchmarkKeyFigureValues on GetBenchmarkKey(c) equals GetBenchmarkKey(bv) into bsg
                from bv in bsg.DefaultIfEmpty() // Left Join benchmark values

                select MapFinal(c, pv, bv)
            ).ToList();
        }
    }

}