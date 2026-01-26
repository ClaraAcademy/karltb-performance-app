using PerformanceApp.Data.Svg.Builders;

namespace PerformanceApp.Data.Test.Svg.Builders;

public class LineBuilderTest
{
    [Fact]
    public void Build_ShouldReturnCorrectXElement()
    {
        // Arrange
        var lineBuilder = new LineBuilder()
            .WithColor("red")
            .WithWidth(2)
            .From(10.5f, 20.5f)
            .To(30.5f, 40.5f);

        // Act
        var result = lineBuilder.Build();

        // Assert
        Assert.Equal("line", result.Name.LocalName);
        Assert.Equal("10.50", result.Attribute("x1")?.Value);
        Assert.Equal("20.50", result.Attribute("y1")?.Value);
        Assert.Equal("30.50", result.Attribute("x2")?.Value);
        Assert.Equal("40.50", result.Attribute("y2")?.Value);
        Assert.Equal("red", result.Attribute("stroke")?.Value);
        Assert.Equal("2", result.Attribute("stroke-width")?.Value);
    }

    [Fact]
    public void Build_WithNoProvidedValues_ShouldUseDefaults()
    {
        // Arrange & Act
        var result = new LineBuilder().Build();

        // Assert
        Assert.Equal("line", result.Name.LocalName);
        Assert.Equal("0.00", result.Attribute("x1")?.Value);
        Assert.Equal("0.00", result.Attribute("y1")?.Value);
        Assert.Equal("0.00", result.Attribute("x2")?.Value);
        Assert.Equal("0.00", result.Attribute("y2")?.Value);
        Assert.Equal("black", result.Attribute("stroke")?.Value);
        Assert.Equal("1", result.Attribute("stroke-width")?.Value);
    }
}