using PerformanceApp.Data.Svg.Scalers.Interface;

namespace PerformanceApp.Data.Svg.Scalers;

public class YScaler(int height, int margin, float max, float min)
    : IScaler
{
    private readonly int _height = height;
    private readonly int _margin = margin;
    private readonly float _max = max;
    private readonly float _min = min;
    public int Height => _height;

    public float Scale(float y)
    {
        return _margin + (_max - y) * (_height - 2 * _margin) / (_max - _min);
    }
    public float Scale(int y) => Scale((float)y);

    public List<float> Scale(IEnumerable<float> ys)
    {
        return ys.Select(Scale).ToList();
    }

    public List<float> Scale(IEnumerable<int> ys)
    {
        return ys.Select(Scale).ToList();
    }
}