using PerformanceApp.Data.Dtos;
using PerformanceApp.Data.Extensions;
using PerformanceApp.Data.Svg.Samplers.Coordinate;
using PerformanceApp.Data.Svg.Samplers.Label;

namespace PerformanceApp.Data.Svg.Samplers;

public class XSampler
{
    private readonly CoordinateSampler _coordinateSampler;
    private readonly LabelSampler _labelSampler;

    public XSampler(IEnumerable<DataPoint2> dataPoints, int width, int margin, int samples)
    {
        var xs = dataPoints
            .GetXs()
            .Distinct()
            .Select(i => (float)i.DayNumber)
            .ToList();
        var min = xs.Min();
        var max = xs.Max();
        _labelSampler = new LabelSampler(min, max, ToLabel, samples);
        _coordinateSampler = new(margin, width - 2 * margin, samples);
    }

    static string ToLabel(float f)
    {
        var i = (int)Math.Floor(f);
        return DateOnly.FromDayNumber(i).ToString();
    }

    public List<string> Labels => _labelSampler.Samples;
    public List<float> Coordinates => _coordinateSampler.Samples;
}
