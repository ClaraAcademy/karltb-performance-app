using System.Xml.Linq;
using PerformanceApp.Data.Svg.Builders;
using PerformanceApp.Data.Svg.Common;
using PerformanceApp.Data.Svg.Defaults;

namespace PerformanceApp.Data.Svg.Models.Abstract;

public abstract class SvgBase(Dimensions dimensions)
{
    protected XElementBuilder SchemaBuilder { get; set; } = SvgDefaults.CreateBaseSchema(dimensions);
    protected virtual XElement Generate() => SchemaBuilder.Build();

    public SvgBase(int width, int height) : this(new(width, height)) { }
    public Dimensions Dimensions { get; } = dimensions;
    public XElement Schema => Generate();
    public override string ToString() => Schema.ToString();
}