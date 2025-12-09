using Microsoft.OpenApi.Models;
using PerformanceApp.Server.Startup.Swagger.Documentation.Constants;

namespace PerformanceApp.Server.Startup.Swagger.Documentation;

using Values = DocumentationConstants;

public static class Documentation
{
    private static readonly OpenApiInfo _info = new()
    {
        Title = Values.Title,
        Version = Values.Version,
        Description = Values.Description
    };

    public static OpenApiInfo Info => _info;
}
