using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Mappers;

public class StagingMapper
{
    public static Staging Map(DateOnly bankday, string instrumentType, string instrumentName, decimal instrumentPrice)
    {
        return new Staging
        {
            Bankday = bankday,
            InstrumentType = instrumentType,
            InstrumentName = instrumentName,
            Price = instrumentPrice
        };
    }
}