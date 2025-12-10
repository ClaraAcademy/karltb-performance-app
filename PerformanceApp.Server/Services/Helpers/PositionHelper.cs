using PerformanceApp.Data.Models;

namespace PerformanceApp.Server.Services.Helpers;

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

    public static async Task<List<T>> GetPositionsAsync<T>(
        Func<DateOnly, int, Task<IEnumerable<Position>>> getPositions,
        Func<Position, T> mapToDto,
        DateOnly bankday,
        int portfolioId
    )
    {
        var positions = await getPositions(bankday, portfolioId);

        return positions
            .Select(mapToDto)
            .ToList();
    }


}