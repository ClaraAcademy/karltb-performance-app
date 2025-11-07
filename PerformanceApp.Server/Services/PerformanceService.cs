using Microsoft.CodeAnalysis.CSharp.Syntax;
using PerformanceApp.Server.DTOs;
using PerformanceApp.Server.Models;
using PerformanceApp.Server.Repositories;
namespace PerformanceApp.Server.Services
{
    public interface IPerformanceService
    {
        Task<List<PortfolioBenchmarkKeyFigureDTO>> GetPortfolioBenchmarkKeyFigureValues(int portfolioId);
    }

    public class PerformanceService(IPerformanceRepository performanceRepository, IPortfolioService portfolioService) : IPerformanceService
    {
        private readonly IPerformanceRepository PerformanceRepository = performanceRepository;
        private readonly IPortfolioService PortfolioService = portfolioService;

        private static PortfolioBenchmarkKeyFigureDTO MapInitial(PortfolioBenchmarkDTO pb, KeyFigureInfo kfi)
            => new PortfolioBenchmarkKeyFigureDTO
            {
                KeyFigureId = kfi.KeyFigureId,
                KeyFigureName = kfi.KeyFigureName,
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
                    PortfolioValue = pv?.KeyFigureValue1,
                    BenchmarkId = c.BenchmarkId,
                    BenchmarkName = c.BenchmarkName,
                    BenchmarkValue = bv?.KeyFigureValue1
                };

        private record Key(int PortfolioId, int KeyFigureId);

        private static Key GetPortfolioKey(PortfolioBenchmarkKeyFigureDTO pb) => new(pb.PortfolioId, pb.KeyFigureId);
        private static Key GetBenchmarkKey(PortfolioBenchmarkKeyFigureDTO pb) => new(pb.BenchmarkId, pb.KeyFigureId);
        private static Key GetPortfolioKey(KeyFigureValue v) => new(v.PortfolioId, v.KeyFigureId);
        private static Key GetBenchmarkKey(KeyFigureValue v) => new(v.PortfolioId, v.KeyFigureId);


        public async Task<List<PortfolioBenchmarkKeyFigureDTO>> GetPortfolioBenchmarkKeyFigureValues(int portfolioId)
        {
            var portfolioBenchmarkDto = await PortfolioService.GetPortfolioBenchmarksAsync(portfolioId);
            var benchmarkId = portfolioBenchmarkDto.Single(pb => pb.PortfolioId == portfolioId).BenchmarkId;

            var keyFigureInfos = await PerformanceRepository.GetKeyFigureInfosAsync();

            var portfolioKeyFigureValues = await PerformanceRepository.GetKeyFigureValuesAsync(portfolioId);
            var benchmarkKeyFigureValues = await PerformanceRepository.GetKeyFigureValuesAsync(benchmarkId);

            var combinations = portfolioBenchmarkDto.SelectMany(pb => keyFigureInfos, MapInitial);

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