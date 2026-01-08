using PerformanceApp.Data.Dtos;
using PerformanceApp.Data.Svg.Extractors;
using PerformanceApp.Data.Svg.Scalers;
using PerformanceApp.Data.Svg.Utilities;

namespace PerformanceApp.Data.Svg.Factories;

public class PointFactory(XExtractor xExtractor, YExtractor yExtractor)
{
    private readonly XExtractor _xExtractor = xExtractor;
    private readonly YExtractor _yExtractor = yExtractor;

    public PointFactory(XScaler xScaler, YScaler yScaler)
        : this(new XExtractor(xScaler), new YExtractor(yScaler))
    {
    }

    private List<string> Create(List<DataPoint2> dataPoints, Func<IEnumerable<DataPoint2>, List<float>> selectYs)
    {
        var xs = _xExtractor.Extract(dataPoints);
        var ys = selectYs(dataPoints);

        return SvgUtilities.MapToPoints(xs, ys);
    }

    public List<string> CreatePrimary(List<DataPoint2> dataPoints)
    {
        return Create(dataPoints, _yExtractor.ExtractY1s);
    }

    public List<string> CreateSecondary(List<DataPoint2> dataPoints)
    {
        return Create(dataPoints, _yExtractor.ExtractY2s);
    }

}