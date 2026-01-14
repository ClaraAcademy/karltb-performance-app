using PerformanceApp.Data.Svg.Samplers.Label;

namespace PerformanceApp.Data.Test.Svg.Samplers.Label;

public class LabelSamplerTest
{
    static string CreateLabel(int i) => $"Label {i}";
    [Fact]
    public void Sample_ShouldReturnExpectedLabels()
    {
        // Arrange
        var n = 10;
        var data = Enumerable
            .Range(0, n)
            .ToList();
        var samples = 4;
        var sampler = new LabelSampler<int>(data, CreateLabel, samples);

        // Act
        var result = sampler.Sample();

        // Assert
        Assert.Equal(samples, result.Count);
        var expectedIndices = Enumerable
            .Range(0, samples)
            .Select(i => (int)Math.Floor(i * (n - 1f) / (samples - 1f)))
            .ToList();
        for (int i = 0; i < samples; i++)
        {
            var expectedLabel = CreateLabel(expectedIndices[i]);
            Assert.Equal(expectedLabel, result[i]);
        }
    }

    [Fact]
    public void EmptyData_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var data = new List<int>();
        var samples = 4;
        var sampler = new LabelSampler<int>(data, CreateLabel, samples);

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => sampler.Sample());
    }
}