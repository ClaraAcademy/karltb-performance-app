using PerformanceApp.Data.Svg.Builders;

namespace PerformanceApp.Data.Test.Svg.Builders;

public class AxisBuilderTest
{
    [Fact]
    public void Build_ShouldReturnCorrectXElement()
    {
        // Arrange
        var axisBuilder = new AxisBuilder()
            .From(5.5f, 10.5f)
            .To(15.5f, 20.5f);

        // Act
        var result = axisBuilder.Build();

        // Assert
        Assert.Equal("line", result.Name.LocalName);
        Assert.Equal("5.50", result.Attribute("x1")?.Value);
        Assert.Equal("10.50", result.Attribute("y1")?.Value);
        Assert.Equal("15.50", result.Attribute("x2")?.Value);
        Assert.Equal("20.50", result.Attribute("y2")?.Value);
        Assert.Equal("black", result.Attribute("stroke")?.Value);
        Assert.Equal("1", result.Attribute("stroke-width")?.Value);
    }

    [Fact]
    public void Build_WithNoProvidedValues_ShouldUseDefaults()
    {
        // Arrange & Act
        var result = new AxisBuilder().Build();

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