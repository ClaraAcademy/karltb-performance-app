using System.Xml.Linq;
using PerformanceApp.Data.Svg.Constants;
using PerformanceApp.Data.Svg.Factories.Core;
using PerformanceApp.Data.Svg.Scalers;

namespace PerformanceApp.Data.Svg.Factories;

public class AxisFactory(int width, int height, int xMargin, int yMargin, int numberOfPoints, float yMax, float yMin)
{
    private readonly LineFactory<float> _lineFactory = new LineFactory<float>(ColorConstants.Black, 1);
    private readonly XScaler _xScaler = new XScaler(width, xMargin, numberOfPoints);
    private readonly YScaler _yScaler = new YScaler(height, yMargin, yMax, yMin);
    private readonly int _width = width;
    private readonly int _height = height;

    public XElement CreateX()
    {
        var x1 = 0;
        var y1 = _yScaler.Scale(0);
        var x2 = _width;
        var y2 = _yScaler.Scale(0);

        return _lineFactory.Create(x1, y1, x2, y2);
    }

    public XElement CreateY()
    {
        var x1 = _xScaler.Scale(0);
        var y1 = 0;
        var x2 = _xScaler.Scale(0);
        var y2 = _height;

        return _lineFactory.Create(x1, y1, x2, y2);
    }
}