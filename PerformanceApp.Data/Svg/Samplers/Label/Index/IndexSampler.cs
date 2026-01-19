using PerformanceApp.Data.Svg.Samplers.Interface;
using PerformanceApp.Data.Svg.Scalers.Interface;
using PerformanceApp.Data.Svg.Scalers.Linear;

namespace PerformanceApp.Data.Svg.Samplers.Label.Index;

public class IndexSampler(IScaler scaler, int nSamples)
    : ISampler<int>
{
    private readonly IScaler _scaler = scaler;
    private readonly int _nSamples = nSamples;

    public IndexSampler(int nTotal, int nSamples)
        : this(new LinearScaler(0, nTotal - 1, nSamples - 1), nSamples)
    { }

    public List<int> Samples => Enumerable
        .Range(0, _nSamples)
        .Select(Sample)
        .ToList();

    int Sample(int index) => (int)Math.Floor(_scaler.Scale(index));
}