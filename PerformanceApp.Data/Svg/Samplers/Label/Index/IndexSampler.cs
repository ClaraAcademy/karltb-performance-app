using PerformanceApp.Data.Svg.Samplers.Uniform;

namespace PerformanceApp.Data.Svg.Samplers.Label.Index;

public class IndexSampler(int total, int samples)
    : UniformSampler<int>(samples)
{
    private readonly int _total = total;
    private readonly int _samples = samples;

    private float StepSize => (_total - 1f) / (_samples - 1f);
    public override int Transform(int index)
    {
        return (int)Math.Floor(index * StepSize);
    }
}