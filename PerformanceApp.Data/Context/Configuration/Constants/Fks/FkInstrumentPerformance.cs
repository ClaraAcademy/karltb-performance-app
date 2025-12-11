using PerformanceApp.Data.Context.Configuration.Constants.Fks.Factory;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Context.Configuration.Constants.Fks;

public static class FkInstrumentPerformance
{
    private static readonly FkFactory _factory = new(nameof(InstrumentPerformance));

    public static string Instrument => _factory.Name(nameof(InstrumentPerformance.InstrumentId));
    public static string PerformanceType => _factory.Name(nameof(InstrumentPerformance.TypeId));
    public static string PeriodStart => _factory.Name(nameof(InstrumentPerformance.PeriodStart));
    public static string PeriodEnd => _factory.Name(nameof(InstrumentPerformance.PeriodEnd));
}