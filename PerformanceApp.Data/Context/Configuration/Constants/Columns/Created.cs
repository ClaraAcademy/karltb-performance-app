using PerformanceApp.Data.Context.Configuration.Constants.Columns.Interface;

namespace PerformanceApp.Data.Context.Configuration.Constants.Columns;

public class Created : IColumn
{
    public const string Name = "Created";
    public const string DefaultValue = "(getdate())";
}