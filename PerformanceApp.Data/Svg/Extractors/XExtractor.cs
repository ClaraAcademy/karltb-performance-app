using PerformanceApp.Data.Dtos;
using PerformanceApp.Data.Svg.Scalers.Interface;

namespace PerformanceApp.Data.Svg.Extractors;

public class XExtractor(IScaler scaler)
{
    private readonly IScaler _xScaler = scaler;

    public List<float> Extract(IEnumerable<DataPoint2> dataPoints)
    {
        return dataPoints
            .Select((_, x) => x)
            .Select(_xScaler.Scale)
            .ToList();
    }

}