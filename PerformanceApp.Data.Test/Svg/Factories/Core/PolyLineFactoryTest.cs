using PerformanceApp.Data.Svg.Constants;
using PerformanceApp.Data.Svg.Defaults;
using PerformanceApp.Data.Svg.Factories.Core;

namespace PerformanceApp.Data.Test.Svg.Factories.Core;

public class PolyLineFactoryTest
{
    static List<string> CreatePoints(int n)
    {
        return Enumerable.Range(1, n)
            .Select(i => $"{i},{i * n}")
            .ToList();
    }

    [Fact]
    public void Create_HandlesEmptyPoints()
    {
        // Arrange
        var points = new List<string>();

        // Act
        var line = PolyLineFactory.Create(points);

        // Assert
        Assert.Equal("polyline", line.Name.LocalName);
        Assert.NotNull(line.Attribute("points"));
        Assert.Equal(string.Empty, line.Attribute("points")!.Value);
    }

    [Fact]
    public void Create_IncludesAllPoints()
    {
        // Arrange
        var n = 100;
        var expected = CreatePoints(n);

        // Act
        var line = PolyLineFactory.Create(expected);

        // Assert
        Assert.Equal("polyline", line.Name.LocalName);
        Assert.NotNull(line.Attribute("points"));
        var actual = line.Attribute("points")!.Value;
        Assert.Equal(actual, string.Join(" ", expected));
    }

    [Fact]
    public void Create_UsesDefaultColorAndWidth()
    {
        // Arrange
        var points = CreatePoints(10);

        // Act
        var line = PolyLineFactory.Create(points);

        // Assert
        Assert.Equal(ColorConstants.Black, line.Attribute("stroke")?.Value);
        Assert.Equal("1", line.Attribute("stroke-width")?.Value);
    }

    [Fact]
    public void Create_HandlesDottedLine()
    {
        // Arrange
        var points = CreatePoints(10);

        // Act
        var line = PolyLineFactory.Create(points, dotted: true);

        // Assert
        Assert.NotNull(line.Attribute("stroke-dasharray"));
    }

    [Fact]
    public void CreatePrimary_UsesPrimaryColorAndWidth()
    {
        // Arrange
        var points = CreatePoints(10);

        // Act
        var line = PolyLineFactory.CreatePrimary(points);

        // Assert
        Assert.Equal(SvgDefaults.PrimaryColor, line.Attribute("stroke")?.Value);
        Assert.Equal("2", line.Attribute("stroke-width")?.Value);
    }

    [Fact]
    public void CreateSecondary_UsesSecondaryColorWidthAndDotted()
    {
        // Arrange
        var points = CreatePoints(10);

        // Act
        var line = PolyLineFactory.CreateSecondary(points);

        // Assert
        Assert.Equal(SvgDefaults.SecondaryColor, line.Attribute("stroke")?.Value);
        Assert.Equal("2", line.Attribute("stroke-width")?.Value);
        Assert.NotNull(line.Attribute("stroke-dasharray"));
    }
}