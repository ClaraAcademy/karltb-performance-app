using PerformanceApp.Data.Svg.Builders;

namespace PerformanceApp.Data.Test.Svg.Builders;

public class PolyLineBuilderTest
{
    [Fact]
    public void Build_ShouldReturnCorrectElement()
    {
        // Arrange
        var points = new List<string> { "10,10", "20,20", "30,30" };
        var polyLineBuilder = new PolyLineBuilder()
            .WithColor("blue")
            .WithWidth(3)
            .IsDotted(true)
            .WithPoints(points);

        // Act
        var result = polyLineBuilder.Build();

        // Assert
        Assert.Equal("polyline", result.Name.LocalName);
        Assert.Equal("10,10 20,20 30,30", result.Attribute("points")?.Value);
        Assert.Equal("none", result.Attribute("fill")?.Value);
        Assert.Equal("blue", result.Attribute("stroke")?.Value);
        Assert.Equal("3", result.Attribute("stroke-width")?.Value);
        Assert.NotNull(result.Attribute("stroke-dasharray"));
    }

}