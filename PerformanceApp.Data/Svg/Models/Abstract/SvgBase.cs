using System.Xml.Linq;
using PerformanceApp.Data.Svg.Builders;
using PerformanceApp.Data.Svg.Common;

namespace PerformanceApp.Data.Svg.Models.Abstract;

public abstract class SvgBase
{
    protected readonly Dimensions _dimensions;
    protected XElement _schema;
    protected XElementBuilder SchemaBuilder;

    public int Width => _dimensions.X;
    public int Height => _dimensions.Y;
    public XElement Schema { get => _schema; set => _schema = value; }

    protected SvgBase(int width, int height)
    {
        _dimensions = new Dimensions(width, height);
        SchemaBuilder = InitializeBuilder();
        _schema = SchemaBuilder.Build();
    }

    private XElementBuilder InitializeBuilder()
    {
        return new XElementBuilder("svg")
            .WithAttribute("width", Width)
            .WithAttribute("height", Height);
    }

    public override string ToString()
    {
        return Schema.ToString();
    }
}