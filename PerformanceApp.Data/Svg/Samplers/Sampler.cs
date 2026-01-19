using System.Xml.Linq;
using PerformanceApp.Data.Dtos;
using PerformanceApp.Data.Extensions;
using PerformanceApp.Data.Svg.Common;
using PerformanceApp.Data.Svg.Defaults;
using PerformanceApp.Data.Svg.Factories;
using PerformanceApp.Data.Svg.Scalers;
using PerformanceApp.Data.Svg.Scalers.Interface;
using PerformanceApp.Data.Svg.Scalers.Linear;

namespace PerformanceApp.Data.Svg.Samplers;

public class Sampler
{
    private readonly XSampler _xSampler;
    private readonly YSampler _ySampler;
    private readonly IScaler _xScaler;
    private readonly IScaler _yScaler;
    private readonly TickFactory _tickFactory = new();
    private readonly LabelFactory _labelFactory = new();

    public Sampler(IEnumerable<DataPoint2> dataPoints, Dimensions dimensions, Margins margins, Samples samples)
    {
        _xSampler = new(dataPoints, dimensions.X, margins.X, samples.X);
        _ySampler = new(dataPoints, dimensions.Y, margins.Y, samples.Y);
        _xScaler = new XScaler(dimensions.X, margins.X, samples.X);
        _yScaler = new YScaler(dimensions.Y, margins.Y, _ySampler.Max, _ySampler.Min);
    }

    float Y0 => _yScaler.Scale(0f);
    float X0 => _xScaler.Scale(0f);
    List<float> Xs => _xSampler.Coordinates;
    List<float> Ys => _ySampler.Coordinates;

    public List<XElement> XTicks => _tickFactory.CreateXs(Xs, Y0);
    public List<XElement> YTicks => _tickFactory.CreateYs(Ys, X0);
    public List<XElement> XLabels => GetXLabels();
    public List<XElement> YLabels => GetYLabels();
    public float MinY => _ySampler.Min;
    public float MaxY => _ySampler.Max;

    static IEnumerable<(float, string)> Combine(IEnumerable<float> coordintates, IEnumerable<string> labels)
    {
        return coordintates
            .Zip(labels)
            .Select(x => (x.First, x.Second));
    }

    List<XElement> GetXLabels()
    {
        var y = Y0 + LabelDefaults.OffsetY;
        var labels = _xSampler.Labels;
        var combined = Combine(Xs, labels);
        var result = _labelFactory.CreateXs(combined, y, "start", 45);
        return result;
    }
    List<XElement> GetYLabels()
    {
        var x = X0 - LabelDefaults.OffsetX;
        var labels = _ySampler.Labels;
        var combined = Combine(Ys, labels);
        var result = _labelFactory.CreateYs(combined, x, "middle");
        return result;
    }
}