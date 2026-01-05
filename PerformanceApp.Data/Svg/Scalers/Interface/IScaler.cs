namespace PerformanceApp.Data.Svg.Scalers.Interface;

public interface IScaler
{
    float Scale(float value);
    float Scale(int value);
    List<float> Scale(IEnumerable<float> values);
    List<float> Scale(IEnumerable<int> values);
}