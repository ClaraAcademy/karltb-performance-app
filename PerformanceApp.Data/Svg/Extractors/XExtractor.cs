using PerformanceApp.Data.Dtos;
using PerformanceApp.Data.Svg.Scalers;

namespace PerformanceApp.Data.Svg.Extractors;

public class XExtractor(XScaler xScaler)
{
    private readonly XScaler _xScaler = xScaler;

    public List<float> Extract(IEnumerable<DataPoint2> dataPoints)
    {
        return dataPoints
            .Select((_, x) => x)
            .Select(_xScaler.Scale)
            .ToList();
    }

}