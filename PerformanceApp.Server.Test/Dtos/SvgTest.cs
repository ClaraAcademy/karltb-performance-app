using PerformanceApp.Server.Dtos;

namespace PerformanceApp.Server.Test.Dtos;

public class SvgTest
{


    [Fact]
    public void Svg_ContainsRootElement()
    {
        // Arrange
        var dataPoints = new List<DataPoint2>
        {
            new DataPoint2(new DateOnly(2023, 1, 1), 0.1f, 0.2f),
            new DataPoint2(new DateOnly(2023, 1, 2), 0.15f, 0.25f),
            new DataPoint2(new DateOnly(2023, 1, 3), 0.2f, 0.3f)
        };
        var svg = new SVG(dataPoints);

        // Act
        var schema = svg.GetSchema();

        // Assert
        Assert.Equal("svg", schema.Name.LocalName);
        Assert.Equal("http://www.w3.org/2000/svg", schema.Name.NamespaceName);
        Assert.Contains(schema.Elements(), e => e.Name.LocalName == "polyline");
    }

    [Fact]
    public void Svg_ContainsPortfolioAndBenchmarkPolylines()
    {
        // Arrange
        var dataPoints = new List<DataPoint2>
        {
            new DataPoint2(new DateOnly(2023, 1, 1), 0.1f, 0.2f),
            new DataPoint2(new DateOnly(2023, 1, 2), 0.15f, 0.25f),
            new DataPoint2(new DateOnly(2023, 1, 3), 0.2f, 0.3f)
        };
        var svg = new SVG(dataPoints);

        // Act
        var schema = svg.GetSchema();
        var polylines = schema.Elements().Where(e => e.Name.LocalName == "polyline").ToList();

        // Assert
        Assert.Equal(2, polylines.Count); // Portfolio and Benchmark lines
        Assert.Contains(polylines, p => p.Attribute("stroke")?.Value == "#211f5e"); // Portfolio
        Assert.Contains(polylines, p => p.Attribute("stroke")?.Value == "#ec646b"); // Benchmark
    }

    [Fact]
    public void Svg_NoDataPoints_SchemaIsEmpty()
    {
        // Arrange
        var dataPoints = new List<DataPoint2>();
        var svg = new SVG(dataPoints);

        // Act
        var schema = svg.GetSchema();

        // Assert
        Assert.Equal("svg", schema.Name.LocalName);
        Assert.Empty(schema.Elements());
    }
}