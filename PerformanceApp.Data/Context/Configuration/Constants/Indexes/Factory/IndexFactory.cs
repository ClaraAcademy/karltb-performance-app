namespace PerformanceApp.Data.Context.Configuration.Constants.Indexes.Factory;

public class IndexFactory(string on)
{
    private readonly string _on = on;
    public string Name(string name) => $"IX_{_on}_{name}";
    public static string Name(string on, string name) => $"IX_{on}_{name}";
}