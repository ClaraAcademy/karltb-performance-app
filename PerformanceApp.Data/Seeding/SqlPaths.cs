namespace PerformanceApp.Data.Seeding;

public static class SqlPaths
{
    private static readonly string Base = Path.Combine(Environment.CurrentDirectory, "Sql");
    public static readonly string StoredProcedures = Path.Combine(Base, "StoredProcedures");
    public static readonly string Functions = Path.Combine(Base, "Functions");
}