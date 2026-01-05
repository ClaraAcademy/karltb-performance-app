using System.Xml.Linq;
using PerformanceApp.Data.Svg.Builders;

namespace PerformanceApp.Data.Svg.Models.Abstract;

public abstract class SvgBase
{
    private readonly int _width;
    private readonly int _height;
    protected XElement _schema;
    protected XElementBuilder SchemaBuilder;

    public int Width => _width;
    public int Height => _height;
    public XElement Schema { get => _schema; set => _schema = value; }

    protected SvgBase(int width, int height)
    {
        _width = width;
        _height = height;
        SchemaBuilder = InitializeBuilder();
        _schema = SchemaBuilder.Build();
    }

    private XElementBuilder InitializeBuilder()
    {
        return new XElementBuilder("svg")
            .WithAttribute("width", _width)
            .WithAttribute("height", _height);
    }

    public override string ToString()
    {
        return Schema.ToString();
    }
}