using System.Xml.Linq;
using PerformanceApp.Data.Svg.Defaults;
using PerformanceApp.Data.Svg.Enums;
using PerformanceApp.Data.Svg.Formatters;

namespace PerformanceApp.Data.Svg.Builders;

public class LabelBuilder
{
    private string _text = string.Empty;
    private float _x = 0f;
    private float _y = 0f;
    private float _offset = 0f;
    private Anchor _anchor = Anchor.Middle;
    private float _angle = 0f;
    private int _size = LabelDefaults.Size;

    public LabelBuilder WithText(string text)
    {
        _text = text;
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
    public LabelBuilder WithSize(int size)
    {
        _size = size;
        return this;
    }
    public LabelBuilder WithOffset(float offset)
    {
        _offset = offset;
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
    public XElement BuildX()
    {
        return Build(_x, _y + _offset, _angle, _anchor, _text);
    }
    public XElement BuildY()
    {
        return Build(_x + _offset, _y, _angle, _anchor, _text);
    }

    public static XElement BuildX(float x, float y, string text)
    {
        return new LabelBuilder()
            .WithX(x)
            .WithY(y)
            .WithText(text)
            .WithAnchor(LabelDefaults.Anchor.X)
            .WithAngle(LabelDefaults.Angle.X)
            .WithOffset(LabelDefaults.Offset.X)
            .BuildX();
    }
    public static XElement BuildY(float x, float y, string text)
    {
        return new LabelBuilder()
            .WithX(x)
            .WithY(y)
            .WithText(text)
            .WithAnchor(LabelDefaults.Anchor.Y)
            .WithAngle(LabelDefaults.Angle.Y)
            .WithOffset(LabelDefaults.Offset.Y)
            .BuildY();
    }

    static XElement Build(float x, float y, float angle, Anchor anchor, string text, int size = LabelDefaults.Size)
    {
        return new XElementBuilder("text")
            .WithAttribute("x", DecimalFormatter.Format(x))
            .WithAttribute("y", DecimalFormatter.Format(y))
            .WithAttribute("text-anchor", anchor.Value)
            .WithAttribute("transform", Rotate(x, y, angle))
            .WithAttribute("font-size", size.ToString())
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