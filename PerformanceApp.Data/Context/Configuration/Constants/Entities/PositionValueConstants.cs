using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Context.Configuration.Constants.Entities;

public class PositionValueConstants : EntityConstants<PositionValue>
{
    private const string _positionIdColumnName = "PositionID";
    private const string _valueColumnName = "PositionValue";
    private const string _valueColumnType = "decimal(18,2)";
    private const string _bankdayForeignKeyName = "FK_PositionValue_Bankday";
    private const string _positionForeignKeyName = "FK_PositionValue_PositionID";

    public static string PositionIdColumnName => _positionIdColumnName;
    public static new string ValueColumnName => _valueColumnName;
    public static new string ValueColumnType => _valueColumnType;
    public static string BankdayForeignKeyName => _bankdayForeignKeyName;
    public static string PositionForeignKeyName => _positionForeignKeyName;
}