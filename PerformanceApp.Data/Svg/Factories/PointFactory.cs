using PerformanceApp.Data.Dtos;
using PerformanceApp.Data.Svg.Extractors;
using PerformanceApp.Data.Svg.Scalers;
using PerformanceApp.Data.Svg.Utilities;

namespace PerformanceApp.Data.Svg.Factories;

public class PointFactory(XScaler xScaler, YScaler yScaler)
{
    private readonly XExtractor _xExtractor = new XExtractor(xScaler);
    private readonly YExtractor _yExtractor = new YExtractor(yScaler);

    public List<string> CreatePrimary(List<DataPoint2> dataPoints)
    {
        var xs = _xExtractor.Extract(dataPoints);
        var ys = _yExtractor.ExtractY1s(dataPoints);

        return MapToPoints(xs, ys);
    }

    public List<string> CreateSecondary(List<DataPoint2> dataPoints)
    {
        var xs = _xExtractor.Extract(dataPoints);
        var ys = _yExtractor.ExtractY2s(dataPoints);

        return MapToPoints(xs, ys);
    }

    private static List<string> MapToPoints(List<float> xs, List<float> ys)
    {
        return xs
            .Zip(ys, SvgUtilities.MapToPoint)
            .ToList();
    }

}