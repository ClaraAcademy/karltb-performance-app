namespace PerformanceApp.Data.Context.Configuration.Constants.Fks.Factory;

public class FkFactory(string from)
{
    private readonly string _from = from;
    public string Name(string to) => $"FK_{_from}_{to}";
    public static string Name(string from, string to) => $"FK_{from}_{to}";
}