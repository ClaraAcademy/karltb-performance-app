using Microsoft.OpenApi.Models;

namespace PerformanceApp.Server.Swagger.Schemes;

public static class BearerRequirementProvider
{
    private static readonly OpenApiSecurityScheme _scheme = new()
    {
        Reference = References.BearerReference.Reference
    };
    public static OpenApiSecurityScheme Scheme => _scheme;
}