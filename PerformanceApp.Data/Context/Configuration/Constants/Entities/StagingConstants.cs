using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Context.Configuration.Constants.Entities;

public class StagingConstants : EntityConstants<Staging>
{
    private const string _instruemntNameColumnName = "InstrumentName";
    private const string _instrumentTypeColumnName = "InstrumentType";
    private const string _priceColumnName = "Price";
    private const string _priceColumnType = "decimal(19, 4)";

    public static string InstrumentNameColumnName => _instruemntNameColumnName;
    public static string InstrumentTypeColumnName => _instrumentTypeColumnName;
    public static string PriceColumnName => _priceColumnName;
    public static string PriceColumnType => _priceColumnType;
}