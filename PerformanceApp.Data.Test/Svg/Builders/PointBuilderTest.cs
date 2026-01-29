using System.Drawing;
using PerformanceApp.Data.Svg.Builders;

namespace PerformanceApp.Data.Test.Svg.Builders;

public class PointBuilderTest
{
    [Fact]
    public void At_WithPointF_SetsPointCorrectly()
    {
        var builder = new PointBuilder();
        var point = new PointF(3.5f, 7.2f);

        var result = builder.At(point);

        Assert.Equal(builder, result);
        Assert.Equal("3.50,7.20", builder.Build());
    }

    [Fact]
    public void At_WithXY_SetsPointCorrectly()
    {
        var builder = new PointBuilder();

        var result = builder.At(1.1f, 2.2f);

        Assert.Equal(builder, result);
        Assert.Equal("1.10,2.20", builder.Build());
    }

    [Fact]
    public void Build_Static_WithXY_ReturnsFormattedString()
    {
        var result = PointBuilder.Build(5.5f, 6.6f);

        Assert.Equal("5.50,6.60", result);
    }

    [Fact]
    public void Build_Static_WithEnumerables_ReturnsFormattedStrings()
    {
        var xs = new List<float> { 1f, 2f, 3f };
        var ys = new List<float> { 4f, 5f, 6f };

        var results = PointBuilder.Build(xs, ys).ToList();

        Assert.Equal(3, results.Count);
        Assert.Equal("1.00,4.00", results[0]);
        Assert.Equal("2.00,5.00", results[1]);
        Assert.Equal("3.00,6.00", results[2]);
    }

    [Fact]
    public void Build_Static_WithEnumerables_DifferentLengths_ZipsToShortest()
    {
        var xs = new List<float> { 1f, 2f };
        var ys = new List<float> { 3f, 4f, 5f };

        var results = PointBuilder.Build(xs, ys).ToList();

        Assert.Equal(2, results.Count);
        Assert.Equal("1.00,3.00", results[0]);
        Assert.Equal("2.00,4.00", results[1]);
    }
}