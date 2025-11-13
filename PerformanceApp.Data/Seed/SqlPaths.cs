namespace PerformanceApp.Data.Seed;

public static class SqlPaths
{
    private static readonly string Base = AppContext.BaseDirectory;
    public static readonly string StoredProcedures = Path.Combine(Base, "StoredProcedures");
    public static readonly string Functions = Path.Combine(Base, "Functions");
}