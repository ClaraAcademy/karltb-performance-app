using PerformanceApp.Data.Dtos;
using PerformanceApp.Data.Svg.Scalers.Interface;

namespace PerformanceApp.Data.Svg.Extractors;

public class YExtractor(IScaler scaler)
{
    private readonly IScaler _yScaler = scaler;

    public List<float> ExtractY1s(IEnumerable<DataPoint2> dataPoints)
    {
        return Extract(dp => dp.Y1, dataPoints);
    }

    public List<float> ExtractY2s(IEnumerable<DataPoint2> dataPoints)
    {
        return Extract(dp => dp.Y2, dataPoints);
    }

    private List<float> Extract(Func<DataPoint2, float> selector, IEnumerable<DataPoint2> dataPoints)
    {
        return dataPoints
            .Select(selector)
            .Select(_yScaler.Scale)
            .ToList();
    }

}