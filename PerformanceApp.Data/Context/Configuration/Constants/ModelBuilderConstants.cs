namespace PerformanceApp.Data.Context.Configuration.Constants;

public class ModelBuilderConstants<T>
{
    private static readonly string _tableName = typeof(T).Name;
    private const string _defaultSchema = "padb";
    private const string _createdColumnName = "Created";
    private const string _createdDefaultValue = "(getdate())";

    public static string TableName => _tableName;
    public static string DefaultSchema => _defaultSchema;
    public static string CreatedColumnName => _createdColumnName;
    public static string CreatedDefaultValue => _createdDefaultValue;
}