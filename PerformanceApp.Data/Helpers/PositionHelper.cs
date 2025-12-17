using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Helpers;

public static class PositionHelper
{
    public static decimal? GetPositionValue(Position p)
    {
        return p
            .PositionValuesNavigation?
            .SingleOrDefault(pv => pv.Bankday == p.Bankday)?
            .Value;
    }

    public static decimal? GetInstrumentUnitPrice(Position p)
    {
        return p
            .InstrumentNavigation?
            .InstrumentPricesNavigation?
            .SingleOrDefault(ip => ip.Bankday == p.Bankday)?
            .Price;
    }

    public static string? GetInstrumentName(Position p)
    {
        return p
            .InstrumentNavigation?
            .Name;
    }
}