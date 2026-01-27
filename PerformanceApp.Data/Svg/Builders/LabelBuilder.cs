using System.Xml.Linq;
using PerformanceApp.Data.Svg.Enums;
using PerformanceApp.Data.Svg.Formatters;

namespace PerformanceApp.Data.Svg.Builders;

public class LabelBuilder
{
    private IEnumerable<string> _texts = [];
    private float _x;
    private float _y;
    private IEnumerable<float> _xs = [];
    private IEnumerable<float> _ys = [];
    private Anchor _anchor = Anchor.Middle;
    private float _angle;

    public LabelBuilder WithTexts(IEnumerable<string> texts)
    {
        _texts = texts;
        return this;
    }
    public LabelBuilder WithX(float x)
    {
        _x = x;
        return this;
    }
    public LabelBuilder WithY(float y)
    {
        _y = y;
        return this;
    }
    public LabelBuilder WithXs(IEnumerable<float> xs)
    {
        _xs = xs;
        return this;
    }
    public LabelBuilder WithYs(IEnumerable<float> ys)
    {
        _ys = ys;
        return this;
    }
    public LabelBuilder WithAnchor(Anchor anchor)
    {
        _anchor = anchor;
        return this;
    }
    public LabelBuilder WithAngle(float angle)
    {
        _angle = angle;
        return this;
    }
    public List<XElement> BuildXs()
    {
        return _xs
            .Zip(_texts, (x, t) => Build(x, _y, _angle, _anchor, t))
            .ToList();
    }
    public List<XElement> BuildYs()
    {
        return _ys
            .Zip(_texts, (y, t) => Build(_x, y, _angle, _anchor, t))
            .ToList();
    }
    static XElement Build(float x, float y, float angle, Anchor anchor, string text)
    {
        return new XElementBuilder("text")
            .WithAttribute("x", DecimalFormatter.Format(x))
            .WithAttribute("y", DecimalFormatter.Format(y))
            .WithAttribute("text-anchor", anchor.Value)
            .WithAttribute("transform", Rotate(x, y, angle))
            .WithValue(text)
            .Build();
    }
    static string Rotate(float x, float y, float angle)
    {
        var fX = DecimalFormatter.Format(x);
        var fY = DecimalFormatter.Format(y);
        var fAngle = DecimalFormatter.Format(angle);
        return $"rotate({fAngle} {fX},{fY})";
    }
}