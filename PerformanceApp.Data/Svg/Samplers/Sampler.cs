using System.Xml.Linq;
using PerformanceApp.Data.Svg.Common;
using PerformanceApp.Data.Svg.Factories;
using PerformanceApp.Data.Svg.Formatters;
using PerformanceApp.Data.Svg.Samplers.Coordinate;
using PerformanceApp.Data.Svg.Samplers.Interface;
using PerformanceApp.Data.Svg.Samplers.Value;
using PerformanceApp.Data.Svg.Scalers.Interface;

namespace PerformanceApp.Data.Svg.Samplers;

public class Sampler(TickFactory tickFactory, LabelFactory labelFactory)
    : ISampler
{
    public static Sampler CreateX(ChartData data, IScaler scaler, int count, float y0)
    {
        var indexes = ValueFactory<int>
            .CreateForIndex(data.PointCount, count)
            .Values;
        var xs = new CoordinateFactory<int>(indexes, scaler.Scale).Coordinates;

        var tickFactory = TickFactory.CreateX(xs, y0);
        var labelFactory = LabelFactory.CreateX(xs, indexes, data.GetXLabel, y0);

        return new(tickFactory, labelFactory);
    }
    public static Sampler CreateY(ChartData data, IScaler scaler, int count, float x0)
    {
        var values = ValueFactory<float>
            .CreateForRange(data.Min, data.Max, count)
            .Values;
        var ys = new CoordinateFactory<float>(values, scaler.Scale).Coordinates;

        var tickFactory = TickFactory.CreateY(ys, x0);
        var labelFactory = LabelFactory.CreateY(ys, values, PercentageFormatter.Format, x0);

        return new(tickFactory, labelFactory);
    }

    public IEnumerable<XElement> Ticks => tickFactory.Ticks;
    public IEnumerable<XElement> Labels => labelFactory.Labels;
}