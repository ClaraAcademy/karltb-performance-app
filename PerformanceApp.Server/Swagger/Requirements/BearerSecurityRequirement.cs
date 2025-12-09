using Microsoft.OpenApi.Models;
using PerformanceApp.Server.Swagger.Schemes;

namespace PerformanceApp.Server.Swagger.Requirements;

public static class BearerSecurityRequirement
{
    private static readonly OpenApiSecurityRequirement _requirement = new()
    {
        { BearerRequirementProvider.Scheme, Array.Empty<string>() }
    };

    public static OpenApiSecurityRequirement Requirement => _requirement;
}