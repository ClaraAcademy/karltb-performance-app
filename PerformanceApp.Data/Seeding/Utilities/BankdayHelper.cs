using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Seeding.Utilities;

public static class BankdayHelper
{
    private static DateOnly GetBankday(DateInfo dateInfo) => dateInfo.Bankday;
    public static List<DateOnly> GetOrderedBankdays(IEnumerable<DateInfo> dateInfos)
    {
        return dateInfos.Select(GetBankday)
           .Distinct()
           .OrderBy(d => d)
           .ToList();
    }

    public static DateOnly GetFirstBankday(IEnumerable<DateInfo> dateInfos)
    {
        return dateInfos.Select(GetBankday).Min();
    }
}