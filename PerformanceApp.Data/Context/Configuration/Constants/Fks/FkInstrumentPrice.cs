using PerformanceApp.Data.Context.Configuration.Constants.Fks.Factory;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Context.Configuration.Constants.Fks;

public static class FkInstrumentPrice
{
    private static readonly FkFactory _factory = new(nameof(InstrumentPrice));

    public static string Instrument => _factory.Name(nameof(InstrumentPrice.InstrumentId));
    public static string Bankday => _factory.Name(nameof(InstrumentPrice.Bankday));
}