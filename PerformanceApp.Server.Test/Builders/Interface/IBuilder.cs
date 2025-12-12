namespace PerformanceApp.Server.Test.Builders.Interface;

public interface IBuilder<T>
{
    T Build();
    T Clone();
    IEnumerable<T> Many(int count);
}