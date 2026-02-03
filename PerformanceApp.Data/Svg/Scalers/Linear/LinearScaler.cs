using PerformanceApp.Data.Svg.Scalers.Interface;

namespace PerformanceApp.Data.Svg.Scalers.Linear;

public record LinearScaler(float Offset, float Step) : IScaler
{
    public virtual float Scale(float value) => Offset + value * Step;
    public virtual float Scale(int value) => Scale((float)value);
}