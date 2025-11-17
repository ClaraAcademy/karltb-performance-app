using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PerformanceApp.Data.Context;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Repositories;

public interface IInstrumentPriceRepository
{
    void AddInstrumentPrices(List<InstrumentPrice> instrumentPrices);
    Task AddInstrumentPricesAsync(List<InstrumentPrice> instrumentPrices);
    Task<IEnumerable<InstrumentPrice>> GetInstrumentPricesAsync();
}

public class InstrumentPriceRepository(PadbContext context) : IInstrumentPriceRepository
{
    private readonly PadbContext _context = context;

    public void AddInstrumentPrices(List<InstrumentPrice> instrumentPrices)
    {
        _context.InstrumentPrices.AddRange(instrumentPrices);
        _context.SaveChanges();
    }

    public async Task AddInstrumentPricesAsync(List<InstrumentPrice> instrumentPrices)
    {
        await _context.InstrumentPrices.AddRangeAsync(instrumentPrices);
        await _context.SaveChangesAsync();
    }
    public async Task<IEnumerable<InstrumentPrice>> GetInstrumentPricesAsync()
    {
        return await _context.InstrumentPrices.ToListAsync();
    }

}