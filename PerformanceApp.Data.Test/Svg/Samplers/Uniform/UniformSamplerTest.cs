using PerformanceApp.Data.Svg.Samplers.Uniform;

namespace PerformanceApp.Data.Test.Svg.Samplers.Uniform;

public class UniformSamplerTest
{
    private const int Factor = 3;
    private class TestSampler(int count)
        : UniformSampler<int>(count)
    {
        public override int Transform(int index) => index * Factor;
    }

    [Fact]
    public void Sample_ShouldReturnExpectedNumberOfSamples()
    {
        // Arrange
        int count = 5;
        var sampler = new TestSampler(count);

        // Act
        var samples = sampler.Sample();

        // Assert
        Assert.Equal(count, samples.Count);
        for (int i = 0; i < count; i++)
        {
            Assert.Equal(i * Factor, samples[i]);
        }
    }

    [Fact]
    public void ZeroCount_ShouldReturnEmptyList()
    {
        // Arrange
        int count = 0;
        var sampler = new TestSampler(count);

        // Act
        var samples = sampler.Sample();

        // Assert
        Assert.Empty(samples);
    }

    [Fact]
    public void SingleCount_ShouldReturnSingleSample()
    {
        // Arrange
        int count = 1;
        var sampler = new TestSampler(count);

        // Act
        var samples = sampler.Sample();

        // Assert
        Assert.Single(samples);
        Assert.Equal(0, samples[0]);
    }

    [Fact]
    public void NegativeCount_ShouldThrowArgumentOutOfRangeException()
    {
        // Arrange
        int count = -5;
        var sampler = new TestSampler(count);

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(sampler.Sample);
    }
}