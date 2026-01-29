using PerformanceApp.Data.Svg.Builders;

namespace PerformanceApp.Data.Test.Svg.Builders;

public class PolyLineBuilderTest
{
    [Fact]
    public void WithWidth_SetsWidthAttribute()
    {
        // Arrange
        var builder = new PolyLineBuilder()
            .WithPoints(["0,0", "10,10"])
            .WithWidth(5);

        // Act
        var element = builder.Build();

        // Assert
        Assert.Equal("polyline", element.Name.LocalName);
        Assert.Equal("5", element.Attribute("stroke-width")?.Value);
    }

    [Fact]
    public void WithColor_SetsStrokeColor()
    {
        var builder = new PolyLineBuilder()
            .WithPoints(["0,0", "10,10"])
            .WithColor("red");

        var element = builder.Build();

        Assert.Equal("red", element.Attribute("stroke")?.Value);
    }

    [Fact]
    public void IsDotted_SetsStrokeDasharray()
    {
        var builder = new PolyLineBuilder()
            .WithPoints(["0,0", "10,10"])
            .IsDotted();

        var element = builder.Build();

        Assert.NotNull(element.Attribute("stroke-dasharray"));
        Assert.False(string.IsNullOrWhiteSpace(element.Attribute("stroke-dasharray")?.Value));
    }

    [Fact]
    public void WithPoints_JoinsPointsCorrectly()
    {
        var points = new[] { "1,2", "3,4", "5,6" };
        var builder = new PolyLineBuilder()
            .WithPoints(points);

        var element = builder.Build();

        Assert.Equal("1,2 3,4 5,6", element.Attribute("points")?.Value);
    }

    [Fact]
    public void Build_StaticMethod_CreatesElementWithGivenParameters()
    {
        var points = new[] { "0,0", "10,10" };
        var element = PolyLineBuilder.Build(points, "blue", true);

        Assert.Equal("polyline", element.Name.LocalName);
        Assert.Equal("blue", element.Attribute("stroke")?.Value);
        Assert.NotNull(element.Attribute("stroke-dasharray"));
    }

    [Fact]
    public void DefaultValues_AreSetCorrectly()
    {
        var builder = new PolyLineBuilder()
            .WithPoints(["0,0", "1,1"]);

        var element = builder.Build();

        Assert.Equal("black", element.Attribute("stroke")?.Value);
        Assert.Equal("2", element.Attribute("stroke-width")?.Value);
        Assert.Equal("none", element.Attribute("fill")?.Value);
    }
}