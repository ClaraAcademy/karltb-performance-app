using PerformanceApp.Data.Svg.Constants;
using PerformanceApp.Data.Svg.Factories.Core;

namespace PerformanceApp.Data.Svg.Defaults;

public class AxisDefaults
{
    public static LineFactory LineFactory => new(ColorConstants.Black, 1);
}