using System.Xml.Linq;
using PerformanceApp.Data.Dtos;
using PerformanceApp.Data.Svg.Builders;
using PerformanceApp.Data.Svg.Defaults;
using PerformanceApp.Data.Svg.Extractors;
using PerformanceApp.Data.Svg.Factories;
using PerformanceApp.Data.Svg.Factories.Core;
using PerformanceApp.Data.Svg.Formatters;
using PerformanceApp.Data.Svg.Models.Abstract;
using PerformanceApp.Data.Svg.Samplers;
using PerformanceApp.Data.Svg.Scalers;
using PerformanceApp.Data.Svg.Utilities;

namespace PerformanceApp.Data.Svg.Models;

public class SvgLineChart : SvgBase
{
    private readonly int _xMargin = SvgDefaults.XMargin;
    private readonly int _yMargin = SvgDefaults.YMargin;
    private readonly XScaler _xScaler;
    private readonly YScaler _yScaler;
    private readonly List<DataPoint2> _dataPoints = [];
    private readonly string _primaryColor = SvgDefaults.PrimaryColor;
    private readonly string _secondaryColor = SvgDefaults.SecondaryColor;
    private readonly AxisFactory _axisFactory;
    private readonly PointFactory _pointFactory;
    private readonly TickFactory _tickFactory;
    private readonly LabelFactory _labelFactory = new();
    private readonly XSampler<DataPoint> _xSampler;
    private readonly YSampler<DataPoint> _ySampler;

    public SvgLineChart(List<DataPoint2> dataPoints, int width, int height)
        : base(width, height)
    {
        var yMin = ValueUtilities.MinY(dataPoints);
        var yMax = ValueUtilities.MaxY(dataPoints);
        _xScaler = new XScaler(Width, _xMargin, dataPoints.Count);
        _yScaler = new YScaler(Height, _yMargin, yMax, yMin);
        _axisFactory = new AxisFactory(_xScaler, _yScaler);
        _dataPoints = dataPoints;
        _pointFactory = new PointFactory(_xScaler, _yScaler);
        _tickFactory = new TickFactory();
        var selectX = new Func<DataPoint, string>(d => d.X.ToString());
        _xSampler = new XSampler<DataPoint>(_xScaler, selectX);
        var selectY = new Func<DataPoint, string>(d => new PercentageFormatter().Format(d.Y));
        _ySampler = new YSampler<DataPoint>(_yScaler, selectY);

        _schema = CreateSchema();
    }

    private static List<DataPoint> Combine(List<DataPoint2> dataPoints)
    {
        var p1s = dataPoints
            .Select(d => new DataPoint(d.X, d.Y1));
        var p2s = dataPoints
            .Select(d => new DataPoint(d.X, d.Y2));
        return p1s
            .Concat(p2s)
            .Distinct()
            .ToList();
    }

    private XElement CreateSchema()
    {
        var xAxis = _axisFactory.CreateX();
        var yAxis = _axisFactory.CreateY();

        var primaryPoints = _pointFactory.CreatePrimary(_dataPoints);
        var primaryLine = PolyLineFactory.CreatePrimary(primaryPoints, _primaryColor);

        var secondaryPoints = _pointFactory.CreateSecondary(_dataPoints);
        var secondaryLine = PolyLineFactory.CreateSecondary(secondaryPoints, _secondaryColor);

        var combined = Combine(_dataPoints);
        var xSamples = _xSampler.Sample(combined.OrderBy(d => d.X), TickDefaults.NumX);
        var ySamples = _ySampler.Sample(combined.OrderBy(d => d.Y), TickDefaults.NumY);

        var xTicks = _tickFactory.CreateXs(xSamples.Select(t => t.Item1), _yScaler.Scale(0));
        var yTicks = _tickFactory.CreateYs(ySamples.Select(t => t.Item1), _xScaler.Scale(0));

        var xLabels = _labelFactory.CreateXs(xSamples, _yScaler.Scale(0) + LabelDefaults.OffsetY, LabelDefaults.Start, LabelDefaults.Angle45);
        var yLabels = _labelFactory.CreateYs(ySamples, _xScaler.Scale(0) - LabelDefaults.OffsetX);

        return SchemaBuilder
            .WithElement(xAxis)
            .WithElement(yAxis)
            .WithElement(primaryLine)
            .WithElement(secondaryLine)
            .WithElements(xTicks)
            .WithElements(yTicks)
            .WithElements(xLabels)
            .WithElements(yLabels)
            .Build();
    }
}