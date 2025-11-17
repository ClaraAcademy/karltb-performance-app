using Microsoft.EntityFrameworkCore;
using PerformanceApp.Data.Context;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Repositories;

public interface IInstrumentPerformanceRepository
{
    Task<IEnumerable<InstrumentPerformance>> GetInstrumentPerformancesAsync();
}

public class InstrumentPerformanceRepository(PadbContext context) : IInstrumentPerformanceRepository
{
    private readonly PadbContext _context = context;

    public async Task<IEnumerable<InstrumentPerformance>> GetInstrumentPerformancesAsync()
    {
        return await _context.InstrumentPerformances.ToListAsync();
    }
}