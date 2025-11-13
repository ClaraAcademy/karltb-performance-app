using Microsoft.EntityFrameworkCore.ChangeTracking;
using PerformanceApp.Data.Context;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Repositories;

public interface IInstrumentPriceRepository
{
    EntityEntry<InstrumentPrice>? AddInstrumentPrice(InstrumentPrice instrumentPrice);
    List<EntityEntry<InstrumentPrice>?> AddInstrumentPrices(List<InstrumentPrice> instrumentPrices);
}

public class InstrumentPriceRepository(PadbContext context) : IInstrumentPriceRepository
{
    private readonly PadbContext _context = context;

    private static bool Equal(InstrumentPrice lhs, InstrumentPrice rhs)
    {
        return lhs.InstrumentId == rhs.InstrumentId
            && lhs.Bankday == rhs.Bankday
            && lhs.Price == rhs.Price;
    }

    private bool Exists(InstrumentPrice instrumentPrice)
    {
        return _context.InstrumentPrices.Any(ip => Equal(ip, instrumentPrice));
    }
    public EntityEntry<InstrumentPrice>? AddInstrumentPrice(InstrumentPrice instrumentPrice)
    {
        return Exists(instrumentPrice) ? null : _context.InstrumentPrices.Add(instrumentPrice);
    }
    public List<EntityEntry<InstrumentPrice>?> AddInstrumentPrices(List<InstrumentPrice> instrumentPrices)
    {
        return instrumentPrices.Select(AddInstrumentPrice).ToList();
    }
}