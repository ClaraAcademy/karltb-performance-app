namespace PerformanceApp.Server.Swagger.Documentation.Constants;

public static class DocumentationConstants
{
    private const int _versionMajor = 0;
    private const int _versionMinor = 1;
    public static readonly string Version = $"v{_versionMajor}.{_versionMinor}";

    public const string Title = "PerformanceApp API";
    public const string Description = "An ASP.NET Core Web API for PerformanceApp";
}