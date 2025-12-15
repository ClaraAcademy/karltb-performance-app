namespace PerformanceApp.Infrastructure.Context.Configuration.Constants;

public class EntityConstants<T>
{
    private static readonly string _tableName = typeof(T).Name;
    private const string _defaultSchema = "padb";
    private const string _createdColumnName = "Created";
    private const string _createdDefaultValue = "(getdate())";
    private const string _idColumnName = "Id";
    private const string _nameColumnName = "Name";
    private const string _valueColumnName = "Value";
    private const string _valueColumnType = "decimal(24,16)";

    public static string TableName => _tableName;
    public static string DefaultSchema => _defaultSchema;
    public static string CreatedColumnName => _createdColumnName;
    public static string CreatedDefaultValue => _createdDefaultValue;
    public static string IdColumnName => _idColumnName;
    public static string NameColumnName => _nameColumnName;
    public static string ValueColumnName => _valueColumnName;
    public static string ValueColumnType => _valueColumnType;
}