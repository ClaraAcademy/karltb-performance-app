namespace PerformanceApp.Data.Seeding;

public static class SqlPaths
{
    private static readonly string Root = Directory.GetParent(AppContext.BaseDirectory)!.Parent!.Parent!.Parent!.Parent!.FullName;
    private static readonly string Base = Path.Combine(Root, "PerformanceApp.Data", "Sql");
    public static readonly string StoredProcedures = Path.Combine(Base, "StoredProcedures");
    public static readonly string Functions = Path.Combine(Base, "Functions");
}