using Microsoft.EntityFrameworkCore;
using PerformanceApp.Data.Context;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Infrastructure.Repositories;

public interface IInstrumentPerformanceRepository
{
    Task<IEnumerable<InstrumentPerformance>> GetInstrumentPerformancesAsync();
    Task AddInstrumentPerformancesAsync(IEnumerable<InstrumentPerformance> instrumentPerformances);
}

public class InstrumentPerformanceRepository(PadbContext context) : IInstrumentPerformanceRepository
{
    private readonly PadbContext _context = context;

    public async Task<IEnumerable<InstrumentPerformance>> GetInstrumentPerformancesAsync()
    {
        return await _context.InstrumentPerformances.ToListAsync();
    }
    public async Task AddInstrumentPerformancesAsync(IEnumerable<InstrumentPerformance> instrumentPerformances)
    {
        await _context.InstrumentPerformances.AddRangeAsync(instrumentPerformances);
        await _context.SaveChangesAsync();
    }
}