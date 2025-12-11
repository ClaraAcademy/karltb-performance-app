using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Context.Configuration.Constants.Entities;

public class InstrumentPerformanceConstants : EntityConstants<InstrumentPerformance>
{
    private const string _instrumentForeignKeyName = "FK_InstrumentPerformance_InstrumentID";
    private const string _performanceTypeForeignKeyName = "FK_InstrumentPerformance_TypeID";
    private const string _periodStartForeignKeyName = "FK_InstrumentPerformance_PeriodStart";
    private const string _periodEndForeignKeyName = "FK_InstrumentPerformance_PeriodEnd";

    public static string InstrumentForeignKeyName => _instrumentForeignKeyName;
    public static string PerformanceTypeForeignKeyName => _performanceTypeForeignKeyName;
    public static string PeriodStartForeignKeyName => _periodStartForeignKeyName;
    public static string PeriodEndForeignKeyName => _periodEndForeignKeyName;
}