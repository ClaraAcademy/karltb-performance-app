using PerformanceApp.Data.Svg.Builders;

namespace PerformanceApp.Data.Test.Svg.Builders;

public class LineBuilderTest
{
    [Fact]
    public void Build_DefaultValues_ShouldReturnBlackLineWithWidth1()
    {
        var builder = new LineBuilder();
        var element = builder.Build();

        Assert.Equal("line", element.Name.LocalName);
        Assert.Equal("Black", element.Attribute("stroke")?.Value);
        Assert.Equal("1", element.Attribute("stroke-width")?.Value);
    }

    [Fact]
    public void WithColor_ShouldSetStrokeColor()
    {
        var builder = new LineBuilder()
            .WithColor("Red");
        var element = builder.Build();

        Assert.Equal("Red", element.Attribute("stroke")?.Value);
    }

    [Fact]
    public void WithWidth_ShouldSetStrokeWidth()
    {
        var builder = new LineBuilder()
            .WithWidth(5);
        var element = builder.Build();

        Assert.Equal("5", element.Attribute("stroke-width")?.Value);
    }

    [Fact]
    public void FromAndTo_ShouldSetCoordinates()
    {
        var builder = new LineBuilder()
            .From(10.5f, 20.5f)
            .To(30.5f, 40.5f);
        var element = builder.Build();

        Assert.Equal("10.50", element.Attribute("x1")?.Value);
        Assert.Equal("20.50", element.Attribute("y1")?.Value);
        Assert.Equal("30.50", element.Attribute("x2")?.Value);
        Assert.Equal("40.50", element.Attribute("y2")?.Value);
    }

    [Fact]
    public void ChainedMethods_ShouldSetAllAttributes()
    {
        var builder = new LineBuilder()
            .WithColor("Blue")
            .WithWidth(3)
            .From(1f, 2f)
            .To(3f, 4f);
        var element = builder.Build();

        Assert.Equal("Blue", element.Attribute("stroke")?.Value);
        Assert.Equal("3", element.Attribute("stroke-width")?.Value);
        Assert.Equal("1.00", element.Attribute("x1")?.Value);
        Assert.Equal("2.00", element.Attribute("y1")?.Value);
        Assert.Equal("3.00", element.Attribute("x2")?.Value);
        Assert.Equal("4.00", element.Attribute("y2")?.Value);
    }
}