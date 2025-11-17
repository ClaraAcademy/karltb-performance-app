using Microsoft.Data.SqlClient;

namespace PerformanceApp.Data.Seeding.Queries;

public static class PerformanceQueries
{
    private static SqlParameter MapToParameter(DateOnly bankday) => new("Bankday", bankday);
    public static FormattableString UpdatePositions(DateOnly bankday)
    {
        return $@"EXEC [padb].[uspUpdatePositions] @Bankday = {MapToParameter(bankday)};";
    }
    public static FormattableString UpdatePortfolioValue(DateOnly bankday)
    {
        return $@"EXEC [padb].[uspUpdatePortfolioValue] @Bankday = {MapToParameter(bankday)};";
    }
    public static FormattableString UpdateInstrumentDayPerformance(DateOnly bankday)
    {
        return $@"EXEC [padb].[uspUpdateInstrumentDayPerformance] @Bankday = {MapToParameter(bankday)};";
    }
    public static FormattableString UpdatePortfolioDayPerformance(DateOnly bankday)
    {
        return $@"EXEC [padb].[uspUpdatePortfolioDayPerformance] @Bankday = {MapToParameter(bankday)};";
    }
    public static FormattableString UpdatePortfolioCumulativeDayPerformance(DateOnly bankday)
    {
        return $@"EXEC [padb].[uspUpdatePortfolioCumulativeDayPerformance] @Bankday = {MapToParameter(bankday)};";
    }
    public static FormattableString UpdatePortfolioMontPerformance()
    {
        return $@"EXEC padb.uspUpdatePortfolioMonthPerformance;";
    }
    public static FormattableString UpdatePortfolioHalfYearPerformance()
    {
        return $@"EXEC padb.uspUpdatePortfolioHalfYearPerformance;";
    }
}