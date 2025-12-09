using Microsoft.OpenApi.Models;
using PerformanceApp.Server.Startup.Swagger.Schemes;

namespace PerformanceApp.Server.Startup.Swagger.Requirements;

public static class BearerSecurityRequirement
{
    private static readonly OpenApiSecurityRequirement _requirement = new()
    {
        { BearerRequirementProvider.Scheme, Array.Empty<string>() }
    };

    public static OpenApiSecurityRequirement Requirement => _requirement;
}