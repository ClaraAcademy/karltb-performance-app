using PerformanceApp.Server.Swagger.Documentation.Constants;
namespace PerformanceApp.Server.Swagger.Constants;

public static class SwaggerConstants
{
    private static readonly string _version = DocumentationConstants.Version;
    public static readonly string Endpoint = $"/swagger/{_version}/swagger.json";
    public static readonly string Name = DocumentationConstants.Title;

}