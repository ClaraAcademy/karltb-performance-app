using PerformanceApp.Data.Svg.Formatters.Base;

namespace PerformanceApp.Data.Svg.Formatters;

public class DecimalFormatter() : Formatter(FormatString)
{
    private const string FormatString = "F2";
}