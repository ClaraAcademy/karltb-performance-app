using System.Xml.Linq;
using PerformanceApp.Data.Svg.Constants;
using PerformanceApp.Data.Svg.Factories.Core;
using PerformanceApp.Data.Svg.Factories.Core.Interfaces;
using PerformanceApp.Data.Svg.Scalers;
using PerformanceApp.Data.Svg.Scalers.Interface;

namespace PerformanceApp.Data.Svg.Factories;

public class AxisFactory(IScaler xScaler, IScaler yScaler, ILineFactory lineFactory, int width, int height)
{
    private readonly ILineFactory _lineFactory = lineFactory;
    private readonly IScaler _xScaler = xScaler;
    private readonly IScaler _yScaler = yScaler;
    private readonly int _width = width;
    private readonly int _height = height;

    private class Defaults
    {
        private class Line
        {
            public const string Color = ColorConstants.Black;
            public const int Width = 1;
        }
        public static readonly ILineFactory LineFactory = new LineFactory(Line.Color, Line.Width);
    }

    public AxisFactory(XScaler xScaler, YScaler yScaler)
        : this(xScaler, yScaler, Defaults.LineFactory, xScaler.Width, yScaler.Height)
    { }


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