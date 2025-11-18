using PerformanceApp.Data.Models;
using PerformanceApp.Data.Repositories;

namespace PerformanceApp.Data.Test.Repositories;

public class InstrumentPerformanceTest
{
    [Fact]
    public async Task GetInstrumentPerformancesAsync_ReturnsPerformances()
    {
        var context = RepositoryTest.GetContext();
        context.InstrumentPerformances.AddRange(new List<InstrumentPerformance>
        {
            new InstrumentPerformance { InstrumentId = 1, TypeId = 1, PeriodStart = DateOnly.FromDateTime(DateTime.Now), PeriodEnd = DateOnly.FromDateTime(DateTime.Now), Value = 0.05m },
            new InstrumentPerformance { InstrumentId = 2, TypeId = 8, PeriodStart = DateOnly.FromDateTime(DateTime.Now), PeriodEnd = DateOnly.FromDateTime(DateTime.Now), Value = 0.10m }
        });
        await context.SaveChangesAsync();

        var repo = new InstrumentPerformanceRepository(context);
        var performances = await repo.GetInstrumentPerformancesAsync();

        Assert.Equal(2, performances.Count());
    }
}