using PerformanceApp.Data.Svg.Scalers.Linear;

namespace PerformanceApp.Data.Svg.Samplers.Value;

public class ValueFactory<T>(Func<int, T> transform, int count)
{
    public IEnumerable<T> Values => Enumerable.Range(0, count).Select(transform);

    public static ValueFactory<int> CreateForIndex(int totalCount, int sampleCount)
    {
        if (sampleCount <= 1)
        {
            throw new ArgumentOutOfRangeException(nameof(sampleCount), "Sample count must be greater than 1.");
        }
        int transform(int i) => (int)(i * (totalCount - 1f) / (sampleCount - 1f));
        return new(transform, sampleCount);
    }
    public static ValueFactory<float> CreateForRange(float min, float max, int count)
    {
        if (count <= 1)
        {
            throw new ArgumentOutOfRangeException(nameof(count), "Count must be greater than 1.");
        }
        float transform(int i) => min + i * (max - min) / (count - 1f);
        return new(transform, count);
    }
}