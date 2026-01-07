using PerformanceApp.Data.Svg.Factories.Core;
using PerformanceApp.Data.Svg.Formatters;

namespace PerformanceApp.Data.Test.Svg.Factories.Core;

public class TextFactoryTest
{
    [Fact]
    public void Create_SetsCorrectAttributes_WithDefaults()
    {
        // Arrange
        var factory = new TextFactory();
        var (x, y) = (10f, 20f);
        var text = "Sample Text";

        // Act
        var textElement = factory.Create(text, x, y);

        // Assert
        Assert.Equal("text", textElement.Name.LocalName);

        var decimalFormatter = new DecimalFormatter();
        Assert.Equal(decimalFormatter.Format(x), textElement.Attribute("x")?.Value);
        Assert.Equal(decimalFormatter.Format(y), textElement.Attribute("y")?.Value);
        Assert.Equal(text, textElement.Value);
    }

    [Fact]
    public void Create_SetsCorrectAttributes()
    {
        // Arrange
        var factory = new TextFactory(16);
        var (x, y) = (15.5f, 25.5f);
        var text = "Another Text";
        var anchor = "start";
        var angle = 45f;

        // Act
        var textElement = factory.Create(text, x, y, anchor, angle);

        // Assert
        Assert.Equal("16", textElement.Attribute("font-size")?.Value);
        Assert.Equal(anchor, textElement.Attribute("text-anchor")?.Value);
        Assert.Equal("rotate(45 15.50,25.50)", textElement.Attribute("transform")?.Value);
    }
}