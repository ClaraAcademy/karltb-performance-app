using System.Xml.Linq;
using PerformanceApp.Data.Svg.Constants;
using PerformanceApp.Data.Svg.Factories.Core;
using PerformanceApp.Data.Svg.Scalers;

namespace PerformanceApp.Data.Svg.Factories;

public class AxisFactory(XScaler xScaler, YScaler yScaler)
{
    private readonly LineFactory _lineFactory = new(ColorConstants.Black, 1);
    private readonly XScaler _xScaler = xScaler;
    private readonly YScaler _yScaler = yScaler;
    private readonly int _width = xScaler.Width;
    private readonly int _height = yScaler.Height;

    public XElement CreateX()
    {
        var x1 = 0;
        var x2 = _width;
        var y = _yScaler.Scale(0);

        return _lineFactory.Create(x1, y, x2, y);
    }

    public XElement CreateY()
    {
        var y1 = 0;
        var y2 = _height;
        var x = _xScaler.Scale(0);

        return _lineFactory.Create(x, y1, x, y2);
    }
}