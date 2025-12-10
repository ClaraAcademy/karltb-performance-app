using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Context.Configuration.Constants.Entities;

public class InstrumentPriceConstants : EntityConstants<InstrumentPrice>
{
    private const string _instrumentForeignKeyName = "FK_InstrumentPrice_InstrumentID";
    private const string _bankdayForeignKeyName = "FK_InstrumentPrice_Bankday";
    private const string _priceColumnType = "decimal(19, 4)";
    private const string _priceColumnName = "Price";
    private const string _instrumentIdColumnName = "InstrumentID";

    public static string InstrumentForeignKeyName => _instrumentForeignKeyName;
    public static string BankdayForeignKeyName => _bankdayForeignKeyName;
    public static string PriceColumnType => _priceColumnType;
    public static string PriceColumnName => _priceColumnName;
    public static string InstrumentIdColumnName => _instrumentIdColumnName;
}