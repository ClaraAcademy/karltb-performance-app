using System.Xml.Linq;
using PerformanceApp.Data.Svg.Builders;
using PerformanceApp.Data.Svg.Enums;

namespace PerformanceApp.Data.Test.Svg.Builders;

public class LabelBuilderTest
{
    [Fact]
    public void BuildY_ShouldReturnXElementWithCorrectAttributes()
    {
        // Arrange
        var builder = new LabelBuilder()
            .WithX(10f)
            .WithY(20f)
            .WithText("TestLabel")
            .WithAnchor(Anchor.Start)
            .WithAngle(45f)
            .WithOffset(5f)
            .WithSize(12);

        // Act
        XElement element = builder.BuildY();

        // Assert
        Assert.Equal("text", element.Name.LocalName);
        Assert.Equal("15.00", element.Attribute("x")?.Value); // 10 + 5 offset
        Assert.Equal("20.00", element.Attribute("y")?.Value);
        Assert.Equal(Anchor.Start.Value, element.Attribute("text-anchor")?.Value);
        Assert.Equal("rotate(45.00 15.00,20.00)", element.Attribute("transform")?.Value);
        Assert.Equal("12", element.Attribute("font-size")?.Value);
        Assert.Equal("TestLabel", element.Value);
    }

    [Fact]
    public void BuildY_DefaultValues_ShouldReturnXElementWithDefaults()
    {
        // Arrange
        var builder = new LabelBuilder()
            .WithX(0f)
            .WithY(0f)
            .WithText("Default");

        // Act
        XElement element = builder.BuildY();

        // Assert
        Assert.Equal("text", element.Name.LocalName);
        Assert.Equal("0.00", element.Attribute("x")?.Value);
        Assert.Equal("0.00", element.Attribute("y")?.Value);
        Assert.Equal(Anchor.Middle.Value, element.Attribute("text-anchor")?.Value);
        Assert.Equal("rotate(0.00 0.00,0.00)", element.Attribute("transform")?.Value);
        Assert.Equal("12", element.Attribute("font-size")?.Value); // Assuming LabelDefaults.Size is 16
        Assert.Equal("Default", element.Value);
    }
}
