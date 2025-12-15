using PerformanceApp.Data.Models;
using PerformanceApp.Infrastructure.Context.Configuration.Constants.Fks.Factory;

namespace PerformanceApp.Infrastructure.Context.Configuration.Constants.Fks;

public static class FkInstrumentPrice
{
    private static readonly FkFactory _factory = new(nameof(InstrumentPrice));

    public static string Instrument => _factory.Name(nameof(InstrumentPrice.InstrumentId));
    public static string Bankday => _factory.Name(nameof(InstrumentPrice.Bankday));
}