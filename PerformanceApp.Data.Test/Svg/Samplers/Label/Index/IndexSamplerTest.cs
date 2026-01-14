using PerformanceApp.Data.Svg.Samplers.Label.Index;

namespace PerformanceApp.Data.Test.Svg.Samplers.Label.Index;

public class IndexSamplerTest
{
    [Fact]
    public void Transform_ShouldReturnExpectedIndex()
    {
        // Arrange
        var total = 10;
        var samples = 4;
        var sampler = new IndexSampler(total, samples);
        var index = 4;
        var expected = (int)Math.Floor(index * (total - 1f) / (samples - 1f));

        // Act
        var actual = sampler.Transform(index);

        // Assert
        Assert.Equal(expected, actual);
    }
}