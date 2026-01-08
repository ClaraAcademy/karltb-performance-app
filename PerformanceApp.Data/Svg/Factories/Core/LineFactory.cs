using System.Xml.Linq;
using PerformanceApp.Data.Svg.Builders;
using PerformanceApp.Data.Svg.Constants;
using PerformanceApp.Data.Svg.Factories.Core.Interfaces;
using PerformanceApp.Data.Svg.Formatters;

namespace PerformanceApp.Data.Svg.Factories.Core;

public class LineFactory(string color, int width) : ILineFactory
{
    private readonly string _color = color;
    private readonly int _width = width;
    private readonly DecimalFormatter _decimalFormatter = new();

    public XElement Create(float x1, float y1, float x2, float y2)
    {
        var c = _decimalFormatter
            .Format([x1, y1, x2, y2])
            .ToArray();

        return new XElementBuilder(XElementConstants.Line)
            .WithAttribute(XAttributeConstants.X1, c[0])
            .WithAttribute(XAttributeConstants.Y1, c[1])
            .WithAttribute(XAttributeConstants.X2, c[2])
            .WithAttribute(XAttributeConstants.Y2, c[3])
            .WithAttribute(XAttributeConstants.Stroke, _color)
            .WithAttribute(XAttributeConstants.StrokeWidth, _width)
            .Build();
    }
}