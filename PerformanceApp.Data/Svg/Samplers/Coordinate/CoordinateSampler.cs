using PerformanceApp.Data.Svg.Samplers.Interface;
using PerformanceApp.Data.Svg.Scalers.Interface;
using PerformanceApp.Data.Svg.Scalers.Linear;

namespace PerformanceApp.Data.Svg.Samplers.Coordinate;

public class CoordinateSampler(IScaler scaler, int count)
    : ISampler<float>
{
    private readonly IScaler _scaler = scaler;
    private readonly int _count = count;

    public CoordinateSampler(int margin, int range, int count)
        : this(new LinearScaler(margin, range, count - 1), count)
    { }

    public List<float> Samples => Enumerable
        .Range(0, _count)
        .Select(_scaler.Scale)
        .ToList();
}