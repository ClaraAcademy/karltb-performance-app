using PerformanceApp.Data.Dtos;
using PerformanceApp.Data.Extensions;
using PerformanceApp.Data.Mappers;
using PerformanceApp.Data.Svg.Formatters;
using PerformanceApp.Data.Svg.Samplers.Coordinate;
using PerformanceApp.Data.Svg.Samplers.Label;
using PerformanceApp.Data.Svg.Scalers;

namespace PerformanceApp.Data.Svg.Samplers;

public class YSampler
{
    private readonly float _min;
    private readonly float _max;
    private readonly LabelSampler _labelSampler;
    private readonly CoordinateSampler _coordinateSampler;
    private static readonly PercentageFormatter _percentageFormatter = new();

    public YSampler(IEnumerable<DataPoint2> dataPoints, int height, int margin, int samples)
    {
        var ys = dataPoints
            .GetYs()
            .ToList();
        _min = ys.Min();
        _max = ys.Max();
        _labelSampler = new LabelSampler(_min, _max, ToLabel, samples);
        _coordinateSampler = new(margin, height - 2 * margin, samples);
    }

    static string ToLabel(float y) => _percentageFormatter.Format(y);

    public float Min => _min;
    public float Max => _max;

    public List<string> Labels => _labelSampler
        .Samples
        .Reverse<string>()
        .ToList();
    public List<float> Coordinates => _coordinateSampler.Samples;
}