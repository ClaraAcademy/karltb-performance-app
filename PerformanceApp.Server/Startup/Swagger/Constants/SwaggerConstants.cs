using PerformanceApp.Server.Startup.Swagger.Documentation.Constants;

namespace PerformanceApp.Server.Startup.Swagger.Constants;

public static class SwaggerConstants
{
    private static readonly string _version = DocumentationConstants.Version;
    public static readonly string Endpoint = $"/swagger/{_version}/swagger.json";
    public static readonly string Name = DocumentationConstants.Title;

    private const string _xmlCommentsFile = "PerformanceApp.Server.xml";
    public static readonly string XmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, _xmlCommentsFile);

}