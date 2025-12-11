using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Context.Configuration.Constants.Entities;

public class PortfolioPerformanceConstants : EntityConstants<PortfolioPerformance>
{
    private const string _portfolioIdForeignKeyName = "FK_PortfolioPerformance_PortfolioID";
    private const string _typeIdForeignKeyName = "FK_PortfolioPerformance_TypeID";
    private const string _periodStartForeignKeyName = "FK_PortfolioPerformance_PeriodStart";
    private const string _periodEndForeignKeyName = "FK_PortfolioPerformance_PeriodEnd";

    public static string PortfolioIdForeignKeyName => _portfolioIdForeignKeyName;
    public static string TypeIdForeignKeyName => _typeIdForeignKeyName;
    public static string PeriodStartForeignKeyName => _periodStartForeignKeyName;
    public static string PeriodEndForeignKeyName => _periodEndForeignKeyName;
}