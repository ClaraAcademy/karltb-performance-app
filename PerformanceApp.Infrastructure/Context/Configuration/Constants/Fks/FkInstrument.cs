using PerformanceApp.Data.Models;
using PerformanceApp.Infrastructure.Context.Configuration.Constants.Fks.Factory;

namespace PerformanceApp.Infrastructure.Context.Configuration.Constants.Fks;

public static class FkInstrument
{
    private static readonly FkFactory _factory = new FkFactory(nameof(Instrument));
    public static string InstrumentType => _factory.Name(nameof(Instrument.TypeId));
}