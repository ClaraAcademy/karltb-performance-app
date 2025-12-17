namespace PerformanceApp.Data.Svg.Scalers;

public class YScaler(int height, int margin, float max, float min)
{
    private readonly int _height = height;
    private readonly int _margin = margin;
    private readonly float _max = max;
    private readonly float _min = min;
    public float Scale(float y)
    {
        return _margin + (_max - y) * (_height - 2 * _margin) / (_max - _min);
    }
}