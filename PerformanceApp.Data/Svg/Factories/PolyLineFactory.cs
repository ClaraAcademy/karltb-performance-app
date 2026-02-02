using System.Xml.Linq;
using PerformanceApp.Data.Svg.Builders;
using PerformanceApp.Data.Svg.Builders.Interfaces;
using PerformanceApp.Data.Svg.Common;
using PerformanceApp.Data.Svg.Samplers.Coordinate;
using PerformanceApp.Data.Svg.Scalers.Interface;

namespace PerformanceApp.Data.Svg.Factories;

public class PolyLineFactory(IPolyLineBuilder builder, IEnumerable<string> points, string color, bool isDotted)
{
    public XElement Line => builder
        .WithPoints(points)
        .WithColor(color)
        .IsDotted(isDotted)
        .Build();

    public static PolyLineFactory FromSeries(ChartSeries series, IScaler xScaler, IScaler yScaler, bool isDotted)
    {
        var xs = new CoordinateFactory<int>(Enumerable.Range(0, series.Count), xScaler.Scale).Coordinates;
        var ys = new CoordinateFactory<float>(series.Values, yScaler.Scale).Coordinates;
        var points = PointFactory.Default(xs, ys).Points;
        var builder = new PolyLineBuilder();
        return new PolyLineFactory(builder, points, series.Color, isDotted);
    }
}

