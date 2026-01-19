using PerformanceApp.Data.Dtos;
using PerformanceApp.Data.Extensions;
using PerformanceApp.Data.Mappers;
using PerformanceApp.Data.Svg.Common;
using PerformanceApp.Data.Svg.Defaults;
using PerformanceApp.Data.Svg.Factories;
using PerformanceApp.Data.Svg.Factories.Core;
using PerformanceApp.Data.Svg.Models.Abstract;
using PerformanceApp.Data.Svg.Samplers;
using PerformanceApp.Data.Svg.Scalers;

namespace PerformanceApp.Data.Svg.Models;

public class SvgLineChart : SvgBase
{
    private readonly Margins _margins = SvgDefaults.Margins;
    private readonly Samples _nSamples = SvgDefaults.Samples;
    private readonly string _primaryColor = SvgDefaults.Color.Primary;
    private readonly string _secondaryColor = SvgDefaults.Color.Secondary;

    public SvgLineChart(List<DataPoint2> dataPoints, int width, int height)
        : base(width, height)
    {
        var sampler = new Sampler(dataPoints, _dimensions, _margins, _nSamples);

        var xScaler = new XScaler(Width, _margins.X, dataPoints.Count);
        var yScaler = new YScaler(Height, _margins.Y, sampler.MaxY, sampler.MinY);

        var axisFactory = new AxisFactory(_dimensions, xScaler, yScaler);

        var points1 = DataPoint2Mapper.FlattenY1s(dataPoints);
        var points2 = DataPoint2Mapper.FlattenY2s(dataPoints);
        var lineFactory1 = new PolyLineFactory(points1, xScaler, yScaler, _primaryColor, 2, false);
        var lineFactory2 = new PolyLineFactory(points2, xScaler, yScaler, _secondaryColor, 2, true);

        _schema = SchemaBuilder
            .WithElement(axisFactory.X)
            .WithElement(axisFactory.Y)
            .WithElement(lineFactory1.PolyLine)
            .WithElement(lineFactory2.PolyLine)
            .WithElements(sampler.XTicks)
            .WithElements(sampler.YTicks)
            .WithElements(sampler.XLabels)
            .WithElements(sampler.YLabels)
            .Build();
    }
}