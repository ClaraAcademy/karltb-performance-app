using System.Xml.Linq;
using PerformanceApp.Data.Dtos;
using PerformanceApp.Data.Extensions;
using PerformanceApp.Data.Svg.Common;
using PerformanceApp.Data.Svg.Defaults;
using PerformanceApp.Data.Svg.Factories;
using PerformanceApp.Data.Svg.Models.Abstract;
using PerformanceApp.Data.Svg.Samplers;

namespace PerformanceApp.Data.Svg.Models;

public class SvgLineChart(ChartData chartData, Dimensions dimensions) : SvgBase(dimensions)
{
    private readonly Margins _margins = SvgDefaults.Margins;
    private readonly Samples _nSamples = SvgDefaults.Samples;

    protected override XElement Generate()
    {
        var scalerFactory = new ScalerFactory(Dimensions, _margins, chartData.PointCount, chartData.Max, chartData.Min);
        var xScaler = scalerFactory.X;
        var yScaler = scalerFactory.Y;

        var axisFactory = AxisFactory.Create(xScaler.Scale(0), yScaler.Scale(0), Dimensions.X, Dimensions.Y);

        var xSampler = Sampler.CreateX(chartData, xScaler, _nSamples.X, yScaler.Scale(0));
        var ySampler = Sampler.CreateY(chartData, yScaler, _nSamples.Y, xScaler.Scale(0));

        var lineFactory1 = PolyLineFactory.FromSeries(chartData.Series[0], xScaler, yScaler, isDotted: false);
        var lineFactory2 = PolyLineFactory.FromSeries(chartData.Series[1], xScaler, yScaler, isDotted: true);

        return SchemaBuilder
            .WithElement(axisFactory.X)
            .WithElement(axisFactory.Y)
            .WithElement(lineFactory1.Line)
            .WithElement(lineFactory2.Line)
            .WithElements(xSampler.Ticks)
            .WithElements(ySampler.Ticks)
            .WithElements(xSampler.Labels)
            .WithElements(ySampler.Labels)
            .Build();
    }

    public SvgLineChart(List<DataPoint2> dataPoints, int width, int height)
        : this(dataPoints.ToChartData(SvgDefaults.Color.Primary, SvgDefaults.Color.Secondary), new(width, height))
    {
    }
}