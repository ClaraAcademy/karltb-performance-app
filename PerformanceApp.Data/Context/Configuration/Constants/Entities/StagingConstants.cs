using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Context.Configuration.Constants.Entities;

public class StagingConstants : EntityConstants<Staging>
{
    private const string _priceColumnType = "decimal(19, 4)";

    public static string PriceColumnType => _priceColumnType;
}