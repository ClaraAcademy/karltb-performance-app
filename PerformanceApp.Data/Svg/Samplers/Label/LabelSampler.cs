using PerformanceApp.Data.Svg.Samplers.Interface;

namespace PerformanceApp.Data.Svg.Samplers.Label;

public class LabelSampler(float min, float max, Func<float, string> toLabel, int nSamples)
    : ISampler<string>
{
    private readonly float _min = min;
    private readonly Func<float, string> _toLabel = toLabel;
    private readonly int _nSamples = nSamples;
    private readonly float _step = (max - min) / (nSamples - 1f);

    public List<string> Samples => Enumerable
        .Range(0, _nSamples)
        .Select(i => _min + i * _step)
        .Select(_toLabel)
        .ToList();
}