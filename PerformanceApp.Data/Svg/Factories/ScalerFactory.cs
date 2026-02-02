using PerformanceApp.Data.Svg.Common;
using PerformanceApp.Data.Svg.Scalers.Index;
using PerformanceApp.Data.Svg.Scalers.Interface;
using PerformanceApp.Data.Svg.Scalers.Value;

namespace PerformanceApp.Data.Svg.Factories;

public class ScalerFactory(Dimensions dimensions, Margins margins, int pointCount, float maxValue, float minValue)
{
    public IScaler X => new IndexScaler(dimensions.X, margins.X, pointCount);
    public IScaler Y => new ValueScaler(dimensions.Y, margins.Y, maxValue, minValue, Inverted: true);
    public static ScalerFactory Create(ChartData data, Dimensions dimensions, Margins margins)
    {
        return new ScalerFactory(dimensions, margins, data.PointCount, data.Max, data.Min);
    }
}