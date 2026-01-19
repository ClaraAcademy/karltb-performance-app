using System.Xml.Linq;
using PerformanceApp.Data.Svg.Common;
using PerformanceApp.Data.Svg.Defaults;
using PerformanceApp.Data.Svg.Factories.Core;
using PerformanceApp.Data.Svg.Scalers;

namespace PerformanceApp.Data.Svg.Factories;

public class AxisFactory(Dimensions dimensions, XScaler xScaler, YScaler yScaler)
{
    private readonly LineFactory _lineFactory = AxisDefaults.LineFactory;
    private readonly Dimensions _dimensions = dimensions;
    private readonly XScaler _xScaler = xScaler;
    private readonly YScaler _yScaler = yScaler;

    float ScaledX => _xScaler.Scale(0f);
    float ScaledY => _yScaler.Scale(0f);

    public XElement X => _lineFactory.Create(0, ScaledY, _dimensions.X, ScaledY);
    public XElement Y => _lineFactory.Create(ScaledX, 0, ScaledX, _dimensions.Y);
}