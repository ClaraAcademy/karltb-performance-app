using System.Drawing;
using System.Xml.Linq;
using PerformanceApp.Data.Svg.Builders.Interfaces;

namespace PerformanceApp.Data.Svg.Builders;

public class PolyLineBuilder : IPolyLineBuilder
{
    private readonly XElementBuilder _elementBuilder = new("polyline");
    private string _color = Color.Black.Name.ToLowerInvariant();
    private int _width = 2;
    private bool _dotted = false;
    private IEnumerable<string> _points = [];

    public IPolyLineBuilder WithColor(string color)
    {
        _color = color;
        return this;
    }
    public IPolyLineBuilder WithWidth(int width)
    {
        _width = width;
        return this;
    }
    public IPolyLineBuilder IsDotted(bool dotted = true)
    {
        _dotted = dotted;
        return this;
    }
    public IPolyLineBuilder WithPoints(IEnumerable<string> points)
    {
        _points = points;
        return this;
    }
    public XElement Build()
    {
        var joined = string.Join(" ", _points);
        _elementBuilder
            .WithAttribute("points", joined)
            .WithAttribute("fill", "none")
            .WithAttribute("stroke", _color)
            .WithAttribute("stroke-width", _width);
        if (_dotted)
        {
            var spacing = PointBuilder.Build(2, 2);
            _elementBuilder
                .WithAttribute("stroke-dasharray", spacing);
        }
        return _elementBuilder.Build();
    }
}