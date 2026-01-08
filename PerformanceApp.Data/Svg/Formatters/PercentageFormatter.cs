using PerformanceApp.Data.Svg.Formatters.Abstract;

namespace PerformanceApp.Data.Svg.Formatters;

public class PercentageFormatter(): Formatter(FormatString)
{
    private const string FormatString = "P0";
}