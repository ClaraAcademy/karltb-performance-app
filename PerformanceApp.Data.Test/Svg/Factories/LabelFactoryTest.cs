using PerformanceApp.Data.Svg.Factories;
using PerformanceApp.Data.Svg.Builders;

namespace PerformanceApp.Data.Test.Svg.Factories;

public class LabelFactoryTest
{
    static string ToString(int i) => i.ToString();
    static string ToString(float v) => v.ToString();
    [Fact]
    public void CreateX_ReturnsCorrectLabels()
    {
        // Arrange
        var xs = new float[] { 10f, 20f, 30f };
        var indexes = new int[] { 1, 2, 3 };
        float y0 = 50f;

        // Act
        var factory = LabelFactory.CreateX(xs, indexes, ToString, y0);
        var labels = factory.Labels.ToList();

        // Assert
        Assert.Equal(3, labels.Count);
        for (int i = 0; i < xs.Length; i++)
        {
            var expected = LabelBuilder.BuildX(xs[i], y0, indexes[i].ToString());
            Assert.Equal(expected.ToString(), labels[i].ToString());
        }
    }

    [Fact]
    public void CreateX_EmptyInputs_ReturnsNoLabels()
    {
        // Arrange
        float y0 = 0f;

        // Act
        var factory = LabelFactory.CreateX([], [], ToString, y0);

        // Assert
        Assert.False(factory.Labels.Any());
    }

    [Fact]
    public void CreateY_ReturnsCorrectLabels()
    {
        // Arrange
        var ys = new float[] { 5f, 15f };
        var values = new float[] { 100f, 200f };
        float x0 = 42f;

        // Act
        var factory = LabelFactory.CreateY(ys, values, ToString, x0);
        var labels = factory.Labels.ToList();

        // Assert
        Assert.Equal(2, labels.Count);
        for (int i = 0; i < ys.Length; i++)
        {
            var expected = LabelBuilder.BuildY(x0, ys[i], values[i].ToString());
            Assert.Equal(expected.ToString(), labels[i].ToString());
        }
    }

    [Fact]
    public void CreateY_EmptyInputs_ReturnsNoLabels()
    {
        // Arrange
        float x0 = 0f;

        // Act
        var factory = LabelFactory.CreateY([], [], ToString, x0);

        // Assert
        Assert.False(factory.Labels.Any());
    }
}