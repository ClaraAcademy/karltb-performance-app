using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PerformanceApp.Data.Context;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Repositories;

public interface IInstrumentPriceRepository
{
    void AddInstrumentPrice(InstrumentPrice instrumentPrice);
    Task AddInstrumentPriceAsync(InstrumentPrice instrumentPrice);
    void AddInstrumentPrices(List<InstrumentPrice> instrumentPrices);
    Task AddInstrumentPricesAsync(List<InstrumentPrice> instrumentPrices);
    Task<IEnumerable<InstrumentPrice>> GetInstrumentPricesAsync();
    IEnumerable<InstrumentPrice> GetInstrumentPrices();
}

public class InstrumentPriceRepository(PadbContext context) : IInstrumentPriceRepository
{
    private readonly PadbContext _context = context;

    public void AddInstrumentPrice(InstrumentPrice instrumentPrice)
    {
        _context.InstrumentPrices.Add(instrumentPrice);
        _context.SaveChanges();
    }

    public async Task AddInstrumentPriceAsync(InstrumentPrice instrumentPrice)
    {
        await _context.InstrumentPrices.AddAsync(instrumentPrice);
        await _context.SaveChangesAsync();
    }

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
    public IEnumerable<InstrumentPrice> GetInstrumentPrices()
    {
        return _context.InstrumentPrices.ToList();
    }

}