using PerformanceApp.Data.Svg.Scalers.Interface;

namespace PerformanceApp.Data.Svg.Scalers.Linear;

public class LinearScaler(float offset, float range, float steps)
    : IScaler
{
    protected readonly float _offset = offset;
    protected readonly float _stepSize = range / steps;

    public virtual float Scale(float value) => _offset + value * _stepSize;
    public virtual float Scale(int value) => Scale((float)value);
}