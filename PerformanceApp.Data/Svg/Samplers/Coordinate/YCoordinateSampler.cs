using PerformanceApp.Data.Svg.Samplers.Uniform;

namespace PerformanceApp.Data.Svg.Samplers.Coordinate;

public class YCoordinateSampler(int count, float margin, float height)
    : UniformSampler<float>(count)
{
    private float Margin => margin;
    private float Height => height;

    float StepSize => (Height - 2 * Margin) / (_count - 1f);
    float Invert(int index) => _count - 1 - index;
    public override float Transform(int index)
    {
        return Margin + Invert(index) * StepSize;
    }
}