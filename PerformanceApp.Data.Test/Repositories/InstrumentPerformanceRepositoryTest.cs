using PerformanceApp.Data.Models;
using PerformanceApp.Data.Repositories;

namespace PerformanceApp.Data.Test.Repositories;

public class InstrumentPerformanceRepositoryTest : BaseRepositoryTest
{
    private readonly InstrumentPerformanceRepository _repository;

    public InstrumentPerformanceRepositoryTest()
    {
        _repository = new InstrumentPerformanceRepository(_context);
    }

    private static List<InstrumentPerformance> CreateInstrumentPerformances()
    {
        return [
            new InstrumentPerformance { InstrumentId = 1, TypeId = 1, PeriodStart = DateOnly.FromDateTime(DateTime.Now), PeriodEnd = DateOnly.FromDateTime(DateTime.Now), Value = 0.05m },
            new InstrumentPerformance { InstrumentId = 2, TypeId = 8, PeriodStart = DateOnly.FromDateTime(DateTime.Now), PeriodEnd = DateOnly.FromDateTime(DateTime.Now), Value = 0.10m }
        ];
    }

    [Fact]
    public async Task GetInstrumentPerformancesAsync_ReturnsPerformances()
    {
        var expected = CreateInstrumentPerformances();
        _context.InstrumentPerformances.AddRange(expected);

        await _context.SaveChangesAsync();

        var actual = await _repository.GetInstrumentPerformancesAsync();

        Assert.Equal(expected.Count, actual.Count());
    }
}