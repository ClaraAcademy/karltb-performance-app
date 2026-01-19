using System.Xml.Linq;
using PerformanceApp.Data.Svg.Constants;
using PerformanceApp.Data.Svg.Utilities;

namespace PerformanceApp.Data.Svg.Builders;

public class PolyLineBuilder
{
    private XElementBuilder _elementBuilder = new(XElementConstants.Polyline);
    private string _color = "black";
    private int _width = 1;
    private bool _dotted = false;
    private List<string> _points = [];
    public PolyLineBuilder WithColor(string color)
    {
        _color = color;
        return this;
    }
    public PolyLineBuilder WithWidth(int width)
    {
        _width = width;
        return this;
    }
    public PolyLineBuilder IsDotted(bool dotted = true)
    {
        _dotted = dotted;
        return this;
    }
    public PolyLineBuilder WithPoints(List<string> points)
    {
        _points = points;
        return this;
    }
    public XElement Build()
    {
        var joinedPoints = string.Join(" ", _points);
        _elementBuilder = _elementBuilder
            .WithAttribute(XAttributeConstants.Points, joinedPoints)
            .WithAttribute(XAttributeConstants.Fill, XAttributeConstants.None)
            .WithAttribute(XAttributeConstants.Stroke, _color)
            .WithAttribute(XAttributeConstants.StrokeWidth, _width);
        if (_dotted)
        {
            var spacing = SvgUtilities.MapToPoint(2, 2);
            _elementBuilder = _elementBuilder
                .WithAttribute(XAttributeConstants.StrokeDashArray, spacing);
        }
        return _elementBuilder.Build();
    }
}