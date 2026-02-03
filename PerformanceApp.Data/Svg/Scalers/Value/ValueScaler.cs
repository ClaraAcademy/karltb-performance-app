using PerformanceApp.Data.Svg.Scalers.Linear;

namespace PerformanceApp.Data.Svg.Scalers.Value;

public record ValueScaler(int Length, int Margin, float Max, float Min, bool Inverted = false)
    : LinearScaler(Margin, (Length - 2f * Margin) / (Max - Min))
{
    public override float Scale(float value)
    {
        if (Inverted)
        {
            return base.Scale(Max - value);
        }
        return base.Scale(value);
    }
}