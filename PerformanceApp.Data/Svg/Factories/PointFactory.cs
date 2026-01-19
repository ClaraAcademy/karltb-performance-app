using PerformanceApp.Data.Dtos;
using PerformanceApp.Data.Svg.Extractors;
using PerformanceApp.Data.Svg.Extractors.Interface;
using PerformanceApp.Data.Svg.Scalers.Interface;
using PerformanceApp.Data.Svg.Utilities;

namespace PerformanceApp.Data.Svg.Factories;

public class PointFactory(IExtractor xExtractor, IExtractor yExtractor)
{
    private readonly IExtractor _xExtractor = xExtractor;
    private readonly IExtractor _yExtractor = yExtractor;

    public PointFactory(IEnumerable<DataPoint> dataPoints, IScaler xScaler, IScaler yScaler)
        : this(new IndexExtractor(dataPoints, xScaler), new ValueExtractor(dataPoints, yScaler, d => d.Y))
    { }

    List<float> Xs => _xExtractor.Coordinates;
    List<float> Ys => _yExtractor.Coordinates;

    public List<string> Points => SvgUtilities.MapToPoints(Xs, Ys);
}