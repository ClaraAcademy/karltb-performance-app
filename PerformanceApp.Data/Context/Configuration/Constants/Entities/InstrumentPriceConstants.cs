using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Context.Configuration.Constants.Entities;

public class InstrumentPriceConstants : EntityConstants<InstrumentPrice>
{
    private const string _instrumentForeignKeyName = "FK_InstrumentPrice_InstrumentID";
    private const string _bankdayForeignKeyName = "FK_InstrumentPrice_Bankday";

    public static string InstrumentForeignKeyName => _instrumentForeignKeyName;
    public static string BankdayForeignKeyName => _bankdayForeignKeyName;
}