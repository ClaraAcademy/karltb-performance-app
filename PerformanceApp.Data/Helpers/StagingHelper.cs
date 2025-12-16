using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Helpers;

public class StagingHelper
{
    public static DateOnly? GetBankday(Staging staging) => staging.Bankday;

}