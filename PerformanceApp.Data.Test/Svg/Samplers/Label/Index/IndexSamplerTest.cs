using Moq;
using PerformanceApp.Data.Svg.Samplers.Label.Index;
using PerformanceApp.Data.Svg.Scalers.Interface;

namespace PerformanceApp.Data.Test.Svg.Samplers.Label.Index;

public class IndexSamplerTest
{
    [Fact]
    public void Samples_ShouldReturnExpectedCount()
    {
        // Arrange
        var mockScaler = new Mock<IScaler>();
        mockScaler
            .Setup(s => s.Scale(It.IsAny<int>()))
            .Returns<int>(i => i); // Identity scaling for simplicity

        int expected = 10;
        var indexSampler = new IndexSampler(mockScaler.Object, expected);

        // Act
        var samples = indexSampler.Samples;

        // Assert
        var actual = samples.Count;
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Samples_ShouldReturnExpectedIndices()
    {
        // Arrange
        int nTotal = 100;
        int nSamples = 5;
        var indexSampler = new IndexSampler(nTotal, nSamples);

        // Act
        var samples = indexSampler.Samples;

        // Assert
        var expectedSamples = new List<int> { 0, 24, 49, 74, 99 };
        Assert.Equal(expectedSamples, samples);
    }
}