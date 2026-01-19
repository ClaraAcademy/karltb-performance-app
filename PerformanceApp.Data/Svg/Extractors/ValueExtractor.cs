using PerformanceApp.Data.Dtos;
using PerformanceApp.Data.Svg.Extractors.Base;
using PerformanceApp.Data.Svg.Scalers.Interface;

namespace PerformanceApp.Data.Svg.Extractors;

public class ValueExtractor(IEnumerable<DataPoint> dataPoints, IScaler scaler, Func<DataPoint, float> selector)
    : Extractor(dataPoints, scaler)
{
    readonly Func<DataPoint, float> _selector = selector;

    protected override List<float> Extract()
    {
        return _dataPoints
            .Select(_selector)
            .Select(_scaler.Scale)
            .ToList();
    }
}