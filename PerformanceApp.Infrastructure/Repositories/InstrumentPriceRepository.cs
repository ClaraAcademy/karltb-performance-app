using Microsoft.EntityFrameworkCore;
using PerformanceApp.Infrastructure.Context;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Infrastructure.Repositories;

public interface IInstrumentPriceRepository
{
    void AddInstrumentPrices(IEnumerable<InstrumentPrice> instrumentPrices);
    Task AddInstrumentPricesAsync(IEnumerable<InstrumentPrice> instrumentPrices);
    Task<IEnumerable<InstrumentPrice>> GetInstrumentPricesAsync();
}

public class InstrumentPriceRepository(PadbContext context) : IInstrumentPriceRepository
{
    private readonly PadbContext _context = context;

    public void AddInstrumentPrices(IEnumerable<InstrumentPrice> instrumentPrices)
    {
        _context.InstrumentPrices.AddRange(instrumentPrices);
        _context.SaveChanges();
    }

    public async Task AddInstrumentPricesAsync(IEnumerable<InstrumentPrice> instrumentPrices)
    {
        await _context.InstrumentPrices.AddRangeAsync(instrumentPrices);
        await _context.SaveChangesAsync();
    }
    public async Task<IEnumerable<InstrumentPrice>> GetInstrumentPricesAsync()
    {
        return await _context.InstrumentPrices.ToListAsync();
    }

}