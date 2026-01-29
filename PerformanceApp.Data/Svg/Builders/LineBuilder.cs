using System.Drawing;
using System.Xml.Linq;
using PerformanceApp.Data.Svg.Formatters;

namespace PerformanceApp.Data.Svg.Builders;

public class LineBuilder
{
    private readonly XElementBuilder _elementBuilder = new("line");
    private string _color = Color.Black.Name;
    private int _width = 1;
    private float _x1;
    private float _y1;
    private float _x2;
    private float _y2;

    public LineBuilder WithColor(string color)
    {
        _color = color;
        return this;
    }
    public LineBuilder WithWidth(int width)
    {
        _width = width;
        return this;
    }
    public LineBuilder From(float x1, float y1)
    {
        _x1 = x1;
        _y1 = y1;
        return this;
    }
    public LineBuilder To(float x2, float y2)
    {
        _x2 = x2;
        _y2 = y2;
        return this;
    }
    public XElement Build()
    {
        return _elementBuilder
            .WithAttribute("x1", DecimalFormatter.Format(_x1))
            .WithAttribute("y1", DecimalFormatter.Format(_y1))
            .WithAttribute("x2", DecimalFormatter.Format(_x2))
            .WithAttribute("y2", DecimalFormatter.Format(_y2))
            .WithAttribute("stroke", _color)
            .WithAttribute("stroke-width", _width)
            .Build();
    }
}