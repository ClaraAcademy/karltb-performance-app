using PerformanceApp.Data.Dtos;
using PerformanceApp.Data.Svg.Extractors.Interface;
using PerformanceApp.Data.Svg.Scalers.Interface;

namespace PerformanceApp.Data.Svg.Extractors.Base;

public abstract class Extractor(IEnumerable<DataPoint> dataPoints, IScaler scaler)
    : IExtractor
{
    protected readonly IEnumerable<DataPoint> _dataPoints = dataPoints;
    protected readonly IScaler _scaler = scaler;

    public List<float> Coordinates => Extract();

    protected abstract List<float> Extract();
}