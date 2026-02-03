using PerformanceApp.Data.Svg.Samplers.Value;

namespace PerformanceApp.Data.Test.Svg.Samplers.Value;

public class ValueFactoryTest
{
    [Fact]
    public void Values_ShouldReturnEmpty_ForZeroCount()
    {
        // Arrange
        var factory = new ValueFactory<int>(i => i, 0);

        // Act
        var values = factory.Values.ToList();

        // Assert
        Assert.Empty(values);
    }

    [Fact]
    public void Values_ShouldReturnTransformedValues()
    {
        // Arrange
        static int transform(int i) => i * 3;
        int count = 4;
        var factory = new ValueFactory<int>(transform, count);

        // Act
        var values = factory.Values.ToList();

        // Assert
        var expected = new[] { 0, 3, 6, 9 };
        Assert.Equal(expected, values);
    }

    [Fact]
    public void CreateForIndex_ReturnsCorrectIndices_ForSampleCountEqualTotalCount()
    {
        // Arrange
        int totalCount = 5;
        int sampleCount = 5;

        // Act
        var factory = ValueFactory<int>.CreateForIndex(totalCount, sampleCount);

        // Assert
        var expected = Enumerable.Range(0, totalCount).ToArray();
        Assert.Equal(expected, factory.Values.ToArray());
    }

    [Fact]
    public void CreateForIndex_ReturnsCorrectIndices_ForSampleCountLessThanTotalCount()
    {
        // Arrange
        int totalCount = 10;
        int sampleCount = 5;

        // Act
        var factory = ValueFactory<int>.CreateForIndex(totalCount, sampleCount);

        // Assert
        var expected = new[] { 0, 2, 4, 6, 9 };
        Assert.Equal(expected, factory.Values.ToArray());
    }

    [Fact]
    public void CreateForIndex_ThrowsException_WhenSampleCountIsOne()
    {
        // Arrange
        var totalCount = 10;
        var sampleCount = 1;
        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            var factory = ValueFactory<int>.CreateForIndex(totalCount, sampleCount);
            var _ = factory.Values.ToArray();
        });
    }

    [Fact]
    public void CreateForRange_ReturnsCorrectValues()
    {
        // Arrange
        float min = 0f;
        float max = 10f;
        int count = 5;

        // Act
        var factory = ValueFactory<float>.CreateForRange(min, max, count);

        // Assert
        var expected = new[] { 0f, 2.5f, 5f, 7.5f, 10f };
        Assert.Equal(expected, factory.Values.ToArray());
    }

    [Fact]
    public void CreateForRange_ThrowsException_WhenCountIsOne()
    {
        // Arrange
        float min = 0f;
        float max = 10f;
        int count = 1;

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            var factory = ValueFactory<float>.CreateForRange(min, max, count);
            var _ = factory.Values.ToArray();
        });
    }
}