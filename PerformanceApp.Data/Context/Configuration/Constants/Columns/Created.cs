namespace PerformanceApp.Data.Context.Configuration.Constants.Columns;

public static class Created
{
    private const string _name = "Created";
    private const string _defaultValue = "(getdate())";
    private const string _sqlType = "datetime2(7)";

    public static string Name => _name;
    public static string DefaultValue => _defaultValue;
    public static string SqlType => _sqlType;
}