using PerformanceApp.Data.Dtos;
using PerformanceApp.Data.Svg.Extractors.Base;
using PerformanceApp.Data.Svg.Scalers.Interface;

namespace PerformanceApp.Data.Svg.Extractors;

public class IndexExtractor(IEnumerable<DataPoint> dataPoints, IScaler indexScaler)
    : Extractor(dataPoints, indexScaler)
{
    static int Index(DataPoint _, int index) => index;

    protected override List<float> Extract()
    {
        return _dataPoints
            .Select(Index)
            .Select(_scaler.Scale)
            .ToList();
    }
}