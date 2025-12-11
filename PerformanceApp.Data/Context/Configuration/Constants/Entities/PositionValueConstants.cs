using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Context.Configuration.Constants.Entities;

public class PositionValueConstants : EntityConstants<PositionValue>
{
    private const string _valueColumnType = "decimal(18,2)";

    public static new string ValueColumnType => _valueColumnType;
}