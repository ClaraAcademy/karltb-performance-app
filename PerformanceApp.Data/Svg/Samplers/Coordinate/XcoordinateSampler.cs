using PerformanceApp.Data.Svg.Samplers.Uniform;

namespace PerformanceApp.Data.Svg.Samplers.Coordinate;

public class XCoordinateSampler(int count, float margin, float width)
    : UniformSampler<float>(count)
{
    private float Margin => margin;
    private float Width => width;

    float StepSize => (Width - 2f * Margin) / (_count - 1f);
    public override float Transform(int index)
    {
        return Margin + index * StepSize;
    }
}