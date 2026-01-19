using PerformanceApp.Data.Svg.Scalers.Linear;

namespace PerformanceApp.Data.Svg.Scalers;

public class YScaler(int height, int margin, float max, float min)
    : LinearScaler(margin, height - 2 * margin, max - min)
{
    private readonly float _max = max;
    public override float Scale(float value) => base.Scale(_max - value);
}