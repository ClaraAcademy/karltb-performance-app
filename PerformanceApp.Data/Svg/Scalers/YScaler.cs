namespace PerformanceApp.Data.Svg.Scalers;

public class YScaler
{
    private readonly float _height;
    private readonly float _margin;
    private readonly float _max;
    private readonly float _min;

    public float ScaleY(float y)
    {
        return _margin + (_max - y) * (_height - 2 * _margin) / (_max - _min);
    }
}