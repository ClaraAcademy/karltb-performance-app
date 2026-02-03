using System.Xml.Linq;
using PerformanceApp.Data.Svg.Models.Abstract;
using PerformanceApp.Data.Svg.Builders;

namespace PerformanceApp.Data.Test.Svg.Models.Abstract;


public class SvgBaseTest
{
    [Fact]
    public void Constructor_InitializesDimensionsAndSchemaBuilder()
    {
        var svg = new SvgBaseTestImpl(100, 200);

        Assert.Equal(100, svg.Dimensions.X);
        Assert.Equal(200, svg.Dimensions.Y);
        Assert.NotNull(svg.GetSchemaBuilder);
    }

    [Fact]
    public void Schema_ReturnsXElement()
    {
        var svg = new SvgBaseTestImpl(50, 50);

        var schema = svg.Schema;

        Assert.IsType<XElement>(schema);
        Assert.Equal("svg", schema.Name.LocalName.ToLower());
    }

    [Fact]
    public void ToString_ReturnsSchemaString()
    {
        var svg = new SvgBaseTestImpl(10, 20);

        string str = svg.ToString();

        Assert.Contains("<svg", str);
        Assert.Contains("width=\"10\"", str);
        Assert.Contains("height=\"20\"", str);
    }

    [Fact]
    public void Generate_ReturnsSameAsSchema()
    {
        var svg = new SvgBaseTestImpl(30, 40);

        var generated = svg.CallGenerate();
        var schema = svg.Schema;

        Assert.Equal(generated.ToString(), schema.ToString());
    }
}

class SvgBaseTestImpl(int width, int height) : SvgBase(width, height)
{
    public XElementBuilder GetSchemaBuilder => SchemaBuilder;
    public XElement CallGenerate() => Generate();
}